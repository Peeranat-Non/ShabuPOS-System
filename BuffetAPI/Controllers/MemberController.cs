using Microsoft.AspNetCore.Mvc;
using BuffetAPI.Data;
using BuffetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BuffetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly BuffetDbContext _context;

        public MemberController(BuffetDbContext context)
        {
            _context = context;
        }

        // POST: api/Member (สมัครสมาชิก - ของเดิม)
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Member member)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                _context.Members.Add(member);
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "สมัครสมาชิกสำเร็จ", memberId = member.MemberId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // ✅✅✅ เพิ่มส่วนนี้: ดึงข้อมูลโปรไฟล์ (GET) ✅✅✅
        // GET: api/Member/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound(new { message = "ไม่พบสมาชิกนี้ในระบบ" });
            }

            return Ok(member);
        }

        // ✅✅✅ เพิ่มส่วนนี้: แก้ไขข้อมูล (PUT) ✅✅✅
        // PUT: api/Member/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] Member member)
        {
            // เช็คว่า ID ที่ส่งมาตรงกับ ID ใน Body ไหม
            if (id != member.MemberId)
            {
                return BadRequest(new { message = "ID ไม่ถูกต้อง" });
            }

            // บอก Entity Framework ว่าข้อมูลนี้มีการแก้ไข
            _context.Entry(member).State = EntityState.Modified;

            try
            {
                // ถ้าไม่ได้ส่ง FirstName มา (เป็น null) ให้กันไว้ก่อน เพราะใน Model บอก Required
                if (string.IsNullOrEmpty(member.FirstName) || string.IsNullOrEmpty(member.Phone))
                {
                    return BadRequest(new { message = "กรุณากรอกชื่อและเบอร์โทรศัพท์" });
                }

                // ป้องกันไม่ให้แก้วันที่สมัคร (RegisterDate)
                _context.Entry(member).Property(x => x.RegisterDate).IsModified = false;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Members.Any(e => e.MemberId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { success = true, message = "บันทึกข้อมูลเรียบร้อย" });
        }

        // GET: api/Member/check-line/U12345...
        [HttpGet("check-line/{lineId}")]
        public async Task<IActionResult> CheckLineMember(string lineId)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.LineUserId == lineId);

            if (member != null)
            {
                // เจอ! ส่งข้อมูลกลับไป
                return Ok(new { isMember = true, memberData = member });
            }
            else
            {
                // ไม่เจอ!
                return Ok(new { isMember = false });
            }
        }
        // =======================================================
        // ✅ ส่วนที่เพิ่มให้: API สำหรับค้นหาด้วย "เบอร์โทรศัพท์" (เพื่อให้ POS เรียกใช้)
        // =======================================================
        // GET: api/Member/search/0812345678
        [HttpGet("search/{phone}")]
        public async Task<IActionResult> SearchByPhone(string phone)
        {
            // ค้นหาสมาชิกที่มีเบอร์โทรตรงกับที่ส่งมา
            var member = await _context.Members
                                       .FirstOrDefaultAsync(m => m.Phone == phone);

            if (member == null)
            {
                return NotFound(new { message = "ไม่พบข้อมูลสมาชิกเบอร์นี้ครับ" });
            }

            // ถ้าเจอ ส่งข้อมูลสมาชิกกลับไปให้ฝั่ง POS
            return Ok(member);
        }
    }
}