# Bootstrap Responsive Classes Reference - PennyWise

## Quick Reference Guide

This document explains all the Bootstrap responsive utility classes used in the PennyWise UI refinement.

---

## 🎯 Display Utilities

### Hide/Show Elements Based on Breakpoint

```html
<!-- ALWAYS HIDDEN on all screen sizes -->
<div class="d-none">Never shows</div>

<!-- HIDDEN on mobile, VISIBLE on desktop -->
<div class="d-none d-md-block">Desktop only</div>

<!-- VISIBLE on mobile, HIDDEN on desktop -->
<div class="d-md-none">Mobile only</div>

<!-- HIDDEN on mobile, VISIBLE on large+ screens -->
<div class="d-none d-lg-block">Sidebar on desktop</div>

<!-- VISIBLE up to tablet, HIDDEN on desktop -->
<div class="d-lg-none">Bottom nav on mobile/tablet</div>
```

**PennyWise Usage:**
- Sidebar: `d-none d-lg-block` (desktop only)
- Bottom nav: `d-lg-none` (mobile/tablet only)
- Dashboard title: `d-none d-md-block` (hide on mobile)

---

## 📏 Sizing Utilities

### Width

```html
<!-- Full width on mobile, auto on desktop -->
<div class="w-100 w-md-auto">Month selector</div>

<!-- Always 100% width -->
<div class="w-100">Full width button</div>

<!-- Auto width (fits content) -->
<div class="w-auto">Fits content</div>
```

**PennyWise Usage:**
- Month navigation: `w-100 w-md-auto`
- Mobile buttons: `w-100 w-md-auto`
- Dropdown: `flex-grow-1 flex-md-grow-0`

### Flexbox Grow

```html
<!-- Grow to fill space on mobile, fixed size on desktop -->
<div class="flex-grow-1 flex-md-grow-0">Dropdown</div>
```

---

## 📦 Spacing Utilities

### Padding

```html
<!-- Small padding on mobile, larger on desktop -->
<div class="p-2 p-md-3">Glass card</div>

<!-- Individual sides -->
<div class="px-2 px-md-3">Horizontal padding</div>
<div class="py-2 py-md-3">Vertical padding</div>

<!-- Specific sides -->
<div class="pt-2 pt-md-3">Top padding</div>
<div class="pb-2 pb-md-3">Bottom padding</div>
```

**Spacing Scale:**
- `p-0` = 0
- `p-1` = 0.25rem (4px)
- `p-2` = 0.5rem (8px) ← **Mobile default**
- `p-3` = 1rem (16px) ← **Desktop default**
- `p-4` = 1.5rem (24px)
- `p-5` = 3rem (48px)

**PennyWise Usage:**
- Glass cards: `p-2 p-md-3`
- Month selector bar: `p-2 p-md-3`
- Buttons: `px-2` (compact mobile)

### Margin

```html
<!-- Small margin on mobile, larger on desktop -->
<div class="mb-3 mb-md-4">Bottom margin</div>

<!-- Individual sides -->
<div class="mt-2 mt-md-3">Top margin</div>
<div class="ms-2 ms-md-3">Start (left) margin</div>
```

**PennyWise Usage:**
- Card spacing: `mb-3 mb-md-4`
- Section gaps: `mb-3 mb-md-4`

### Gap (Flexbox/Grid)

```html
<!-- Small gap on mobile, larger on desktop -->
<div class="d-flex gap-2 gap-md-3">Flex container</div>

<!-- Grid row spacing -->
<div class="row g-2 g-md-3">Grid system</div>
```

**PennyWise Usage:**
- Stat cards: `row g-2 g-md-3`
- Month navigation: `gap-2 gap-md-3`

---

## 🎨 Layout Utilities

### Grid System

```html
<!-- 2 columns on mobile (50% each), 4 on desktop (25% each) -->
<div class="row g-2 g-md-3">
  <div class="col-6 col-md-3">Card 1</div>
  <div class="col-6 col-md-3">Card 2</div>
  <div class="col-6 col-md-3">Card 3</div>
  <div class="col-6 col-md-3">Card 4</div>
</div>

<!-- Full width on mobile, half on tablet, third on desktop -->
<div class="row">
  <div class="col-12 col-sm-6 col-md-4">Item</div>
</div>
```

**PennyWise Usage:**
- Dashboard stats: `col-6 col-md-3` (2x2 on mobile, 4 across on desktop)

### Flexbox Direction

```html
<!-- Stack vertically on mobile, horizontal on desktop -->
<div class="d-flex flex-column flex-md-row">
  <div>Item 1</div>
  <div>Item 2</div>
</div>
```

**PennyWise Usage:**
- Month selector wrapper: `flex-column flex-md-row`
- Card headers: `flex-column flex-md-row`

### Alignment

```html
<!-- Top align on mobile, center on desktop -->
<div class="align-items-start align-items-md-center">...</div>

<!-- Left align on mobile, space-between on desktop -->
<div class="justify-content-start justify-content-md-between">...</div>
```

**PennyWise Usage:**
- Month selector: `align-items-start align-items-md-center`

---

## 📱 Table Utilities

### Responsive Columns

```html
<table class="modern-table">
  <thead>
    <tr>
      <!-- Hidden on mobile, visible on medium+ -->
      <th class="d-none d-md-table-cell">Date</th>
      
      <!-- Always visible -->
      <th>Category</th>
      <th>Amount</th>
      
      <!-- Hidden on mobile, visible on small+ -->
      <th class="d-none d-sm-table-cell">Description</th>
    </tr>
  </thead>
</table>
```

**PennyWise Usage:**
- Expenses table:
  - Date: `d-none d-md-table-cell` (desktop only)
  - Category: Always visible
  - Amount: Always visible
  - Description: `d-none d-sm-table-cell` (hide on smallest phones)

---

## 🎯 Common Patterns in PennyWise

### Pattern 1: Compact Mobile Header

```html
<div class="glass-card mb-3 mb-md-4 p-2 p-md-3">
  <div class="d-flex flex-column flex-md-row gap-2 gap-md-3">
    <div class="d-none d-md-block">
      <h1>Title</h1>
      <p>Subtitle</p>
    </div>
    <div class="d-flex gap-2 w-100 w-md-auto">
      <!-- Navigation controls -->
    </div>
  </div>
</div>
```

**What it does:**
- Mobile: Compact, no title, full-width controls
- Desktop: Title + subtitle, normal-width controls

### Pattern 2: Responsive Button

```html
<a class="btn btn-modern btn-sm w-100 w-md-auto">
  Add Item
</a>
```

**What it does:**
- Mobile: Full-width button (easier to tap)
- Desktop: Auto-width button (fits content)

### Pattern 3: Hide Sidebar, Show Bottom Nav

```html
<!-- Sidebar: Desktop only -->
<aside class="app-sidebar d-none d-lg-block">
  <!-- Navigation menu -->
</aside>

<!-- Bottom Nav: Mobile only -->
<nav class="bottom-nav d-lg-none">
  <!-- Icon navigation -->
</nav>
```

**What it does:**
- Mobile: Bottom nav visible, sidebar hidden
- Desktop: Sidebar visible, bottom nav hidden

### Pattern 4: Responsive Grid Cards

```html
<div class="row g-2 g-md-3 mb-3 mb-md-4">
  <div class="col-6 col-md-3">
    <div class="glass-card p-3">Card 1</div>
  </div>
  <div class="col-6 col-md-3">
    <div class="glass-card p-3">Card 2</div>
  </div>
  <div class="col-6 col-md-3">
    <div class="glass-card p-3">Card 3</div>
  </div>
  <div class="col-6 col-md-3">
    <div class="glass-card p-3">Card 4</div>
  </div>
</div>
```

**What it does:**
- Mobile: 2x2 grid with small gaps
- Desktop: 4 across with larger gaps

---

## 📐 Breakpoint Reference

| Breakpoint | Screen Width | Bootstrap Class Infix | Usage in PennyWise |
|------------|--------------|----------------------|---------------------|
| Extra small | < 576px | (none) | Default styles |
| Small | ≥ 576px | `sm` | Hide some table columns |
| Medium | ≥ 768px | `md` | Show titles, adjust layout |
| Large | ≥ 992px | `lg` | **Show sidebar, hide bottom nav** |
| Extra large | ≥ 1200px | `xl` | (not used) |
| XXL | ≥ 1400px | `xxl` | (not used) |

---

## 🔧 How to Read Bootstrap Classes

### Format: `{property}{sides}-{breakpoint}-{size}`

**Examples:**
- `p-2` = padding all sides, size 2 (0.5rem)
- `px-md-3` = padding left+right, medium+ screens, size 3 (1rem)
- `mb-3` = margin bottom, size 3 (1rem)
- `mb-md-4` = margin bottom, medium+ screens, size 4 (1.5rem)

**Sides:**
- (blank) = all sides
- `t` = top
- `b` = bottom
- `s` = start (left in LTR)
- `e` = end (right in LTR)
- `x` = horizontal (left + right)
- `y` = vertical (top + bottom)

---

## 💡 Pro Tips

### Mobile-First Approach

```html
<!-- WRONG: Desktop-first -->
<div class="d-block d-md-none">Mobile content</div>

<!-- RIGHT: Mobile-first -->
<div class="d-md-none">Mobile content</div>
```

**Why?** Default styles apply to mobile, use breakpoint classes to modify for larger screens.

### Combining Classes

```html
<!-- Multiple responsive modifiers -->
<div class="p-2 p-md-3 p-lg-4">
  Grows padding as screen gets larger
</div>

<!-- Multiple properties -->
<div class="mb-3 mb-md-4 px-2 px-md-3">
  Bottom margin AND horizontal padding
</div>

<!-- Width + Display -->
<div class="w-100 w-md-auto d-none d-md-block">
  Full width AND hidden on mobile
</div>
```

### Testing Breakpoints

1. Open browser DevTools (F12)
2. Toggle device toolbar (Ctrl+Shift+M)
3. Test these widths:
   - 375px (iPhone)
   - 768px (Tablet)
   - 992px (Desktop)
   - 1440px (Large desktop)

---

## 📚 Reference URLs

- **Bootstrap Docs:** https://getbootstrap.com/docs/5.3/utilities/
- **Display Utilities:** https://getbootstrap.com/docs/5.3/utilities/display/
- **Spacing:** https://getbootstrap.com/docs/5.3/utilities/spacing/
- **Flex:** https://getbootstrap.com/docs/5.3/utilities/flex/
- **Grid:** https://getbootstrap.com/docs/5.3/layout/grid/

---

## 🎓 Practice Examples

### Challenge: Make a Responsive Card

```html
<!-- Goal: 
  - Mobile: Full width, small padding, stacked content
  - Desktop: Auto width, larger padding, horizontal content
-->

<div class="glass-card p-2 p-md-3 mb-3 mb-md-4">
  <div class="d-flex flex-column flex-md-row gap-2 gap-md-3">
    <h5 class="mb-0">Title</h5>
    <button class="btn btn-primary w-100 w-md-auto">
      Action
    </button>
  </div>
</div>
```

### Challenge: Responsive Table

```html
<table class="table">
  <thead>
    <tr>
      <th class="d-none d-md-table-cell">ID</th>
      <th>Name</th>
      <th>Amount</th>
      <th class="d-none d-sm-table-cell">Status</th>
    </tr>
  </thead>
</table>
```

**Result:**
- Extra small: Shows Name, Amount only
- Small+: Shows Name, Amount, Status
- Medium+: Shows all columns including ID

---

## ✅ Quick Checklist for New Responsive Components

When adding a new component, ask:

- [ ] Does it need to hide on mobile/desktop? → Use `d-none d-{breakpoint}-block`
- [ ] Should it be full-width on mobile? → Use `w-100 w-md-auto`
- [ ] Does padding need to be smaller on mobile? → Use `p-2 p-md-3`
- [ ] Should content stack vertically on mobile? → Use `flex-column flex-md-row`
- [ ] Does it need smaller gaps on mobile? → Use `gap-2 gap-md-3`
- [ ] Should margins be compact on mobile? → Use `mb-3 mb-md-4`
- [ ] Do table columns need to hide on mobile? → Use `d-none d-md-table-cell`

---

**Remember:** Start with mobile design, enhance for desktop! 📱➡️🖥️
