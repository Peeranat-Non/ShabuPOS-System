# ShabuPOS-System 🍲
ระบบบริหารจัดการร้านอาหารประเภทชาบูและบุฟเฟต์แบบครบวงจร (All-in-One POS Solution) ที่ครอบคลุมทั้งการจัดการหน้าร้าน การสั่งอาหารผ่านหน้าเว็บ และระบบบัญชีหลังบ้าน

---

## 🚀 ภาพรวมของระบบ (Project Overview)
ระบบถูกออกแบบมาเป็น **Multiproject Solution** เพื่อแยกการทำงานตามหน้าที่ของบุคลากรภายในร้าน โดยแบ่งออกเป็น 3 ส่วนหลัก:

1.  **BuffetAPI:** ระบบหลังบ้าน (Backend) พัฒนาด้วย ASP.NET Core และ Entity Framework Core สำหรับจัดการข้อมูลและเชื่อมต่อฐานข้อมูล SQL Server
2.  **ShabuPOS (Desktop):** โปรแกรมสำหรับพนักงานและผู้บริหาร พัฒนาด้วย C# WinForms สำหรับจัดการโต๊ะ, แคชเชียร์, บัญชี และสต็อกสินค้า
3.  **Web Ordering (Frontend):** เว็บไซต์สำหรับลูกค้าสั่งอาหารผ่านมือถือ พัฒนาด้วย Next.js และ TypeScript

---

## ✨ คุณสมบัติเด่น (Key Features)
* **Table Management:** แผนผังโต๊ะอาหารแบบ Real-time แบ่งสถานะตามสี (เขียว=ว่าง / แดง=มีลูกค้า)
* **Staff Roles:** แยกสิทธิ์การใช้งานตามตำแหน่ง (ผู้จัดการ, เจ้าหน้าที่บัญชี, พนักงานต้อนรับ, แคชเชียร์)
* **Inventory & Stock:** ระบบจัดการวัตถุดิบ บันทึกใบขอซื้อ (PR) และใบสั่งซื้อ (PO)
* **Accounting:** ระบบบันทึกรายรับ-รายจ่ายหลัก และรายจ่ายย่อย พร้อมสรุปยอดภาษี (VAT 7%)
* **Report Generation:** สามารถส่งออกรายงานเป็นไฟล์ PDF และ Excel (.xlsx) ได้ทันที
* **Digital Membership:** ระบบค้นหาสมาชิกและใช้โปรโมชั่นส่วนลดผ่านเบอร์โทรศัพท์
* **Web Ordering:** ลูกค้าสามารถสแกน QR Code เพื่อเลือกแพ็คเกจและสั่งอาหารผ่านหน้าเว็บ

---

## 🛠️ เทคโนโลยีที่ใช้ (Tech Stack)
* **Backend:** ASP.NET Core (C#), Entity Framework Core
* **Desktop:** Windows Forms (C#), ClosedXML (Excel Export)
* **Web Frontend:** Next.js, Tailwind CSS, TypeScript, pnpm
* **Database:** Microsoft SQL Server
* **Tunneling:** Microsoft Dev Tunnels, ngrok

---

## ⚙️ การติดตั้งและเริ่มต้นใช้งาน (Installation & Setup)

### 1. การเตรียมฐานข้อมูล (Database)
* ทำการ Attach หรือ Restore ฐานข้อมูล SQL Server
* แก้ไข Connection String ในโปรเจกต์ **BuffetAPI** ให้ตรงกับชื่อ Server ของคุณ

### 2. การเปิดระบบ Backend & Desktop (Visual Studio 2022)
1.  เปิดไฟล์ `BuffetAPI.sln`
2.  ทำการ **Build Solution** เพื่อ Restore NuGet Packages
3.  รันโปรเจกต์ **BuffetAPI** (ระบุ Startup Project)
4.  กดยืนยันหน้าเว็บ **Dev Tunnels** และคัดลอก URL ไปใส่ในโปรเจกต์ Web (ในส่วนการเรียก API)
5.  รันโปรเจกต์ **ShabuPOS** เพื่อใช้งานระบบจัดการร้าน

### 3. การเปิดระบบ Web Frontend (VS Code)
1.  เปิดโฟลเดอร์โปรเจกต์เว็บผ่าน VS Code
2.  ติดตั้ง pnpm (ถ้ายังไม่มี): `npm install -g pnpm`
3.  ติดตั้ง Dependencies: `pnpm install`
4.  รันโปรเจกต์: `pnpm dev`
5.  เปิด Tunnel (เพื่อทดสอบบนมือถือ): `ngrok http 3000`

---

## 📊 โครงสร้างโฟลเดอร์ (Project Structure)
```text
ShabuPOS-System/
├── BuffetAPI/          # ระบบหลังบ้านจัดการข้อมูล (API)
├── ShabuPOS/           # โปรแกรมจัดการหน้าร้าน (WinForms Desktop)
├── Buffet-Ordering/    # หน้าเว็บสำหรับสั่งอาหาร (Next.js Frontend)
├── BuffetAPI.sln       # ไฟล์ Solution หลัก
└── README.md           # เอกสารประกอบโครงการ
