ShabuPOS-System 🍲
ระบบบริหารจัดการร้านอาหารประเภทชาบูและบุฟเฟต์แบบครบวงจร (All-in-One POS Solution) ที่ครอบคลุมทั้งการจัดการหน้าร้าน การสั่งอาหารผ่านเว็บ และระบบบัญชีหลังบ้าน

🚀 ภาพรวมของระบบ (Project Overview)
ระบบถูกออกแบบมาเป็น Multiproject Solution เพื่อแยกการทำงานตามหน้าที่ของบุคลากรภายในร้าน โดยแบ่งออกเป็น 3 ส่วนหลัก:

BuffetAPI: ระบบหลังบ้าน (Backend) พัฒนาด้วย ASP.NET Core และ Entity Framework Core สำหรับจัดการข้อมูลและเชื่อมต่อฐานข้อมูล SQL Server

ShabuPOS (Desktop): โปรแกรมสำหรับพนักงานและผู้บริหาร พัฒนาด้วย C# WinForms สำหรับจัดการโต๊ะ, แคชเชียร์, บัญชี และสต็อกสินค้า

Web Ordering (Frontend): เว็บไซต์สำหรับลูกค้าสั่งอาหารผ่านมือถือ พัฒนาด้วย Next.js และ TypeScript

✨ คุณสมบัติเด่น (Key Features)
Table Management: แผนผังโต๊ะอาหารแบบ Real-time แบ่งสถานะตามสี (ว่าง/มีลูกค้า)

Staff Roles: แยกสิทธิ์การใช้งานตามตำแหน่ง (ผู้จัดการ, บัญชี, พนักงานต้อนรับ, แคชเชียร์)

Inventory & Stock: ระบบจัดการวัตถุดิบ ใบขอซื้อ (PR) และใบสั่งซื้อ (PO)

Accounting: ระบบบันทึกรายรับ-รายจ่าย พร้อมการออกรายงานสรุปผล (PDF/Excel)

Digital Membership: ค้นหาข้อมูลสมาชิกและใช้โปรโมชั่นส่วนลดผ่านระบบ

Web Ordering: ลูกค้าสามารถสแกน QR Code เพื่อสั่งอาหารผ่านหน้าเว็บได้ทันที

🛠️ เทคโนโลยีที่ใช้ (Tech Stack)
Backend: .NET 6/8, Entity Framework Core

Desktop: C# Windows Forms (.NET Framework / .NET)

Web: Next.js, Tailwind CSS, pnpm

Database: Microsoft SQL Server

Connectivity: Microsoft Dev Tunnels, ngrok

⚙️ การติดตั้งและเริ่มต้นใช้งาน (Installation & Setup)
1. การเตรียมฐานข้อมูล (Database)
ทำการ Attach หรือ Restore ฐานข้อมูล SQL Server

แก้ไข Connection String ในไฟล์ appsettings.json (ใน BuffetAPI) ให้ตรงกับเครื่องของคุณ

2. การเปิดระบบ Backend & Desktop (Visual Studio 2022)
เปิดไฟล์ BuffetAPI.sln

ทำการ Rebuild Solution เพื่อติดตั้ง NuGet Packages

รันโปรเจกต์ BuffetAPI เพื่อเปิดอุโมงค์ข้อมูล (Dev Tunnels)

กดยืนยันหน้าเว็บ Dev Tunnels (Continue) และคัดลอก URL ไปใส่ในโปรเจกต์ Web

รันโปรเจกต์ ShabuPOS เพื่อเริ่มใช้งานโปรแกรมจัดการร้าน

3. การเปิดระบบ Web Frontend (VS Code)
เปิดโฟลเดอร์โปรเจกต์เว็บ

ติดตั้ง pnpm (ถ้ายังไม่มี): npm install -g pnpm

ติดตั้ง Dependencies: pnpm install

รันโปรเจกต์: pnpm dev

เปิด Tunnel (ถ้าต้องการทดสอบบนมือถือ): ngrok http 3000
