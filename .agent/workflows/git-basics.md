---
description: Git version control basics for PennyWise project
---

# Git Basics Workflow for PennyWise

## 🎯 What is Git?
Git is a **version control system** that tracks changes to your code over time. Think of it as a "save game" system for your code where you can:
- Save snapshots of your work (commits)
- Go back to previous versions
- Collaborate with others
- Keep your code safe on GitHub

## 📊 The Three Areas in Git

```
Working Directory  →  Staging Area  →  Repository (Local)  →  GitHub (Remote)
  (your files)       (git add)         (git commit)          (git push)
```

## 🔑 Essential Git Commands

### 1. Check Status (What's Changed?)
```bash
git status
```
Shows which files are modified, staged, or untracked.

### 2. Add Files to Staging Area
```bash
# Add a specific file
git add Controllers/AccountController.cs

# Add all changes in current directory
git add .

# Add all C# files
git add *.cs
```

### 3. Commit Changes (Save Snapshot)
```bash
git commit -m "Add user authentication feature"
```
**Good commit messages:**
- ✅ "Add login functionality to AccountController"
- ✅ "Fix bug in expense calculation"
- ✅ "Update dashboard UI for mobile responsiveness"

**Bad commit messages:**
- ❌ "update"
- ❌ "fixes"
- ❌ "changes"

### 4. Push to GitHub (Upload)
```bash
# Push to main branch
git push origin main

# First time push (set upstream)
git push -u origin main
```

### 5. Pull from GitHub (Download)
```bash
git pull origin main
```

### 6. View Commit History
```bash
# Short format
git log --oneline

# Detailed format
git log

# Last 5 commits
git log -5
```

## 🚀 Daily Workflow

### Scenario 1: Making Changes and Pushing to GitHub
```bash
# 1. Check current status
git status

# 2. Add your changes
git add .

# 3. Commit with a meaningful message
git commit -m "Add expense filtering by category"

# 4. Push to GitHub
git push origin main
```

### Scenario 2: Getting Latest Code from GitHub
```bash
# Pull latest changes before starting work
git pull origin main
```

### Scenario 3: Checking What Changed
```bash
# See what files changed
git status

# See actual code changes
git diff

# See changes in a specific file
git diff Controllers/AccountController.cs
```

## 🌿 Branching Basics (For Later)

Branches let you work on features without affecting the main code.

```bash
# Create a new branch
git branch feature/add-reports

# Switch to that branch
git checkout feature/add-reports

# Create and switch in one command
git checkout -b feature/add-reports

# List all branches
git branch

# Merge branch into main
git checkout main
git merge feature/add-reports

# Delete branch after merging
git branch -d feature/add-reports
```

## 🔧 Useful Commands

### Undo Changes
```bash
# Discard changes in a file (before staging)
git restore Controllers/AccountController.cs

# Unstage a file (after git add)
git restore --staged Controllers/AccountController.cs

# Undo last commit (keep changes)
git reset --soft HEAD~1

# Undo last commit (discard changes) - CAREFUL!
git reset --hard HEAD~1
```

### View Remote Repository
```bash
# See GitHub URL
git remote -v

# Change GitHub URL
git remote set-url origin https://github.com/BPoojitha22/pennywise.git
```

## 📝 Best Practices

1. **Commit Often**: Make small, logical commits instead of one huge commit
2. **Write Clear Messages**: Explain WHAT and WHY, not HOW
3. **Pull Before Push**: Always pull latest changes before pushing
4. **Use .gitignore**: Don't commit temporary files, passwords, or build artifacts
5. **Review Before Commit**: Use `git status` and `git diff` to review changes

## 🚨 Common Issues & Solutions

### Issue: "Your branch is ahead of origin/main"
**Solution:** You have local commits not on GitHub. Push them:
```bash
git push origin main
```

### Issue: "Your branch is behind origin/main"
**Solution:** GitHub has newer code. Pull it:
```bash
git pull origin main
```

### Issue: "Merge conflict"
**Solution:** Git can't auto-merge. Edit conflicted files, then:
```bash
git add .
git commit -m "Resolve merge conflict"
```

### Issue: "Permission denied" when pushing
**Solution:** Set up authentication (Personal Access Token or SSH key)

## 🎓 Learning Path

1. **Week 1**: Master `status`, `add`, `commit`, `push`, `pull`
2. **Week 2**: Learn branching and merging
3. **Week 3**: Practice resolving conflicts
4. **Week 4**: Explore advanced features (rebase, cherry-pick, stash)

## 📚 Quick Reference

| Task | Command |
|------|---------|
| Check status | `git status` |
| Add all changes | `git add .` |
| Commit | `git commit -m "message"` |
| Push to GitHub | `git push origin main` |
| Pull from GitHub | `git pull origin main` |
| View history | `git log --oneline` |
| Create branch | `git checkout -b branch-name` |
| Switch branch | `git checkout branch-name` |
| Merge branch | `git merge branch-name` |
| Discard changes | `git restore filename` |

## 🔗 Resources
- [Git Official Documentation](https://git-scm.com/doc)
- [GitHub Guides](https://guides.github.com/)
- [Interactive Git Tutorial](https://learngitbranching.js.org/)
