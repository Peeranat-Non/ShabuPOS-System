namespace BuffetAPI.Models
{
    public class BillResult
    {
        public string ServiceId { get; set; }
        public string Package_Name { get; set; }
        public string PackageId { get; set; }
        public int TotalPeople { get; set; }

        public decimal Package_Price { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal Subtotal { get; set; }
        public decimal PromoDiscAmount { get; set; }
        public decimal MemberDiscAmount { get; set; }
        public decimal VatAmount { get; set; }
        public int TableNo { get; set; }
        public decimal GrandTotal { get; set; }
        public int OrderHeaderId { get; set; }

        // ⭐ ฟังก์ชันคำนวณบิลมาตรฐาน (ใช้ทั้งระบบ)
        public static BillResult Calculate(decimal price, int qty, decimal promoPct, decimal memPct)
        {
            var res = new BillResult();

            res.Package_Price = price;
            res.TotalPeople = qty;

            res.Subtotal = price * qty;

            res.PromoDiscAmount = res.Subtotal * (promoPct / 100m);
            res.MemberDiscAmount = res.Subtotal * (memPct / 100m);

            decimal net = res.Subtotal - res.PromoDiscAmount - res.MemberDiscAmount;

            res.VatAmount = Math.Round(net * 0.07m, 2, MidpointRounding.AwayFromZero);
            res.GrandTotal = net + res.VatAmount;

            return res;
        }
    }
}