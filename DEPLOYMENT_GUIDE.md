# 🚀 PennyWise - Secure Azure Deployment Guide

## 🔒 Security First: Understanding Configuration

### The Problem We Just Fixed
Your `appsettings.json` had the production database password hardcoded:
```
Password=penny@123
```

**Why is this dangerous?**
- ❌ Anyone with access to your code repository can see it
- ❌ If committed to GitHub, it's publicly visible
- ❌ Violates security best practices for production apps

### The Solution: Environment-Based Configuration

ASP.NET Core reads configuration in this **priority order** (highest to lowest):
1. **Environment Variables** (Azure App Service Configuration) ⭐ **HIGHEST PRIORITY**
2. `appsettings.{Environment}.json` (e.g., `appsettings.Production.json`)
3. `appsettings.json`
4. User Secrets (Development only)

**Result:** Even if `appsettings.json` has a dummy password, Azure will **override** it with the real one from Configuration settings.

---

## 📋 Pre-Deployment Checklist

### Your Current Azure Resources
Based on your connection string, you have:
- **SQL Server**: `pennywise-server-pooja.database.windows.net`
- **Database**: `PennyWiseDB`
- **Admin User**: `pennyadmin`
- **Password**: `penny@123` (we'll secure this)

### What You Need
- [ ] Azure subscription (you already have this)
- [ ] Azure SQL Database (you already have this)
- [ ] Azure App Service (create if not exists)
- [ ] Visual Studio 2022

---

## 🎯 Step-by-Step Deployment Process

### Phase 1: Verify/Create Azure App Service

1. **Log into Azure Portal**: https://portal.azure.com

2. **Check if App Service exists**:
   - Search for "App Services" in the top search bar
   - Look for an existing app service for PennyWise
   
3. **If it doesn't exist, create one**:
   - Click **"+ Create"** → **"Web App"**
   - **Subscription**: Your subscription
   - **Resource Group**: Create new or use existing (e.g., `PennyWise-RG`)
   - **Name**: `pennywise-app-pooja` (must be globally unique)
   - **Publish**: Code
   - **Runtime stack**: .NET 8 (or .NET 6/7 depending on your project)
   - **Operating System**: Windows
   - **Region**: Choose closest to you (e.g., Central India, East US)
   - **Pricing Plan**: 
     - For testing: **F1 (Free)** - Limited resources
     - For production: **B1 (Basic)** - ~$13/month
   - Click **"Review + Create"** → **"Create"**

---

### Phase 2: Configure Connection String in Azure (THE CRITICAL STEP)

This is where we **securely inject** the database password.

1. **Navigate to your App Service** in Azure Portal

2. **Go to Configuration**:
   - Left menu → **Settings** → **Configuration**

3. **Add Connection String**:
   - Click **"+ New connection string"**
   - **Name**: `DefaultConnection` (MUST match exactly with Program.cs!)
   - **Value**: Paste this (update password if needed):
     ```
     Server=tcp:pennywise-server-pooja.database.windows.net,1433;Initial Catalog=PennyWiseDB;Persist Security Info=False;User ID=pennyadmin;Password=penny@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
     ```
   - **Type**: **SQLServer**
   - **Deployment slot setting**: Leave unchecked

4. **Save**:
   - Click **OK**
   - Click **Save** at the top
   - Click **Continue** when prompted

**🎉 Result:** Your password is now stored securely in Azure, NOT in your code!

---

### Phase 3: Configure SQL Server Firewall

Your Azure SQL Server needs to allow connections from your App Service.

1. **Go to Azure Portal** → Search for **"SQL servers"**

2. **Select**: `pennywise-server-pooja`

3. **Networking** (left menu under Security):
   - **Public network access**: Enabled
   - **Firewall rules**:
     - ✅ Check **"Allow Azure services and resources to access this server"**
     - Add your current IP if you want to connect from Visual Studio:
       - Click **"+ Add your client IPv4 address"**
   - Click **Save**

---

### Phase 4: Publish from Visual Studio

1. **Open Visual Studio** → Open PennyWise solution

2. **Right-click** on the **PennyWise** project (not the solution) → **Publish**

3. **Target**: 
   - Select **Azure**
   - Click **Next**

4. **Specific Target**:
   - Select **Azure App Service (Windows)**
   - Click **Next**

5. **App Service**:
   - Sign in with your Azure account if prompted
   - Select your subscription
   - Expand resource groups and find your App Service
   - Select it
   - Click **Finish**

6. **Publish Profile Created**:
   - You'll see a publish profile screen
   - Click **Publish** button (top right)

7. **Wait for Deployment**:
   - Visual Studio will:
     - Build the project
     - Package the files
     - Upload to Azure
     - Start the app
   - Watch the Output window for progress

8. **Success**:
   - Browser should automatically open to your app
   - URL will be: `https://pennywise-app-pooja.azurewebsites.net`

---

### Phase 5: Verify Deployment

1. **Test the Application**:
   - Navigate to: `https://your-app-name.azurewebsites.net`
   - Try to register a new user
   - Log in
   - Add income/expenses

2. **Check Logs** (if something fails):
   - Azure Portal → Your App Service
   - Left menu → **Monitoring** → **Log stream**
   - Watch for errors in real-time

3. **Common Issues**:

   **Issue**: "Cannot open server" error
   - **Fix**: Check SQL Server firewall rules (Phase 3)

   **Issue**: "Connection string not found"
   - **Fix**: Verify connection string name is exactly `DefaultConnection` in Azure Configuration

   **Issue**: 500 Internal Server Error
   - **Fix**: Enable detailed errors temporarily:
     - App Service → Configuration → Application settings
     - Add: `ASPNETCORE_ENVIRONMENT` = `Development` (temporarily)
     - Restart app
     - Check error details
     - **IMPORTANT**: Change back to `Production` after debugging!

---

## 🔐 Security Best Practices

### ✅ What We Did Right
1. **Removed password from code** - No secrets in `appsettings.json`
2. **Used Azure Configuration** - Secrets stored in Azure Key Vault-backed configuration
3. **Environment-specific settings** - Different configs for Dev/Prod

### 🎯 Additional Security Recommendations

1. **Change Default Password**:
   ```sql
   -- Connect to Azure SQL via Azure Portal Query Editor
   ALTER LOGIN pennyadmin WITH PASSWORD = 'NewStrongP@ssw0rd!2024';
   ```
   Then update Azure App Service Configuration with new password.

2. **Use Azure Key Vault** (Advanced):
   - Store connection string in Key Vault
   - Reference it from App Service
   - Provides audit logs and automatic rotation

3. **Enable HTTPS Only**:
   - App Service → Settings → Configuration → General settings
   - **HTTPS Only**: On

4. **Disable Remote Debugging**:
   - App Service → Settings → Configuration → General settings
   - **Remote Debugging**: Off

---

## 💰 Cost Management

### Current Resources & Costs
- **SQL Database**: 
  - Basic tier: ~$5/month
  - Standard S0: ~$15/month
- **App Service**:
  - F1 (Free): $0 (limited, auto-sleeps)
  - B1 (Basic): ~$13/month

### How to Stop Services (To Avoid Charges)

**Stop App Service**:
```
Azure Portal → App Service → Overview → Click "Stop"
```

**Pause SQL Database** (not available in all tiers):
```
Azure Portal → SQL Database → Overview → Click "Pause"
```

**Delete Everything** (if done testing):
```
Azure Portal → Resource Groups → Select your RG → "Delete resource group"
```

---

## 🎤 Interview Questions You Can Now Answer

### Q1: "How do you secure database passwords in production?"
**Answer**: "I use environment-based configuration. In ASP.NET Core, I store connection strings in Azure App Service Configuration, which overrides appsettings.json at runtime. This way, secrets never exist in source code or repositories. For even higher security, I'd use Azure Key Vault with managed identities."

### Q2: "Explain the deployment process for an ASP.NET Core app to Azure."
**Answer**: "First, I create an Azure App Service and SQL Database. I configure the connection string in App Service Configuration (not in code). I ensure SQL Server firewall allows Azure services. Then I publish from Visual Studio using the Azure publish profile, which builds, packages, and deploys the app. Finally, I verify by testing the live URL and checking logs."

### Q3: "What's the difference between appsettings.json and appsettings.Production.json?"
**Answer**: "appsettings.json contains base configuration for all environments. appsettings.Production.json contains production-specific overrides. ASP.NET Core merges them at runtime based on the ASPNETCORE_ENVIRONMENT variable. Production settings take precedence over base settings."

### Q4: "How does ASP.NET Core know which environment it's running in?"
**Answer**: "It reads the ASPNETCORE_ENVIRONMENT environment variable. In Azure App Service, this is automatically set to 'Production'. Locally in Visual Studio, it's set to 'Development' via launchSettings.json. The app uses this to load the correct appsettings file and enable/disable features like detailed error pages."

---

## 📝 Quick Reference Commands

### Check Current Environment (in code)
```csharp
var env = app.Environment.EnvironmentName; // "Development" or "Production"
if (app.Environment.IsDevelopment()) { /* ... */ }
```

### Connection String Priority (Highest to Lowest)
1. Azure App Service Configuration
2. Environment Variables
3. appsettings.Production.json (if ASPNETCORE_ENVIRONMENT=Production)
4. appsettings.json

### Verify Deployment
```
URL: https://your-app-name.azurewebsites.net
Logs: Azure Portal → App Service → Log stream
```

---

## ✅ Post-Deployment Checklist

- [ ] App loads without errors
- [ ] Can register new user (database write works)
- [ ] Can log in (authentication works)
- [ ] Can add income/expense (CRUD operations work)
- [ ] HTTPS is enforced
- [ ] ASPNETCORE_ENVIRONMENT is set to "Production"
- [ ] No passwords in appsettings.json
- [ ] Connection string is in Azure Configuration only

---

## 🆘 Troubleshooting

### "The page isn't working" / HTTP 500
1. Enable detailed errors (see Phase 5)
2. Check Log Stream in Azure Portal
3. Verify connection string is correct
4. Check SQL Server firewall

### "Cannot connect to database"
1. Verify SQL Server firewall allows Azure services
2. Test connection string locally
3. Check if database exists
4. Verify credentials are correct

### App shows old version after publishing
1. App Service → Overview → Restart
2. Clear browser cache
3. Check publish output for errors

---

## 🎓 Next Steps

1. **Set up CI/CD**: Use GitHub Actions or Azure DevOps for automatic deployments
2. **Add Application Insights**: Monitor performance and errors
3. **Configure Custom Domain**: Use your own domain instead of azurewebsites.net
4. **Set up Staging Slots**: Test changes before pushing to production

---

**Created**: 2026-02-15  
**Last Updated**: 2026-02-15  
**Author**: Deployment Guide for Interview Prep
