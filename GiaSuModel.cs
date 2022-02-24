using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_GiaSu.Models
{
    public enum EnumMonDay
    {
        [Description("Toán")]
        Toan = 1,
        [Description("Lý")]
        Ly = 2,
        [Description("Hóa")]
        Hoa = 3,
        [Description("Văn")]
        Van = 4,
        [Description("Tiếng Anh")]
        TiengAnh = 5,
        [Description("Sinh")]
        Sinh = 6,
        [Description("Báo bài")]
        BaoBai = 7,
        [Description("Sử")]
        Su = 8,
        [Description("Tiếng Việt")]
        TiengViet = 9,

        [Description("Địa")]
        Dia = 10,
        [Description("Vẽ")]
        Ve = 11,
        [Description("Đàn nhạc")]
        DanNhac = 12,
        [Description("Tin học")]
        TinHoc = 13,
        [Description("Rèn chữ đẹp")]
        RenChuDep = 14,
        [Description("Tiếng Hoa")]
        TiengHoa = 15,
        [Description("Tiếng Nhật")]
        TiengNhat = 16,
        [Description("Anh văn giao tiếp")]
        AnhVanGiaoTiep = 17,
        [Description("Tiếng Hàn")]
        TiengHan = 18,

        [Description("Kế toán")]
        KeToan = 19,
        [Description("Tiếng Nga")]
        TiengNga = 20,
        [Description("Tiếng Pháp")]
        TiengPhap = 21,
        [Description("Tiếng Đức")]
        TiengDuc = 22,
        [Description("Tiếng Campuchia")]
        TiengCampuchia = 23,
        [Description("Tiếng Thái")]
        TiengThai = 24,
        [Description("Tiếng Ý")]
        TiengY = 25,
        [Description("Môn khác")]
        MonKhac = 26,
        [Description("Luyện thi đại học")]
        LTDH = 27
    }
    public enum EnumLopDay
    {
        [Description("Lớp lá")]
        LopLa = 0,
        [Description("Lớp 1")]
        Lop1 = 1,
        [Description("Lớp 2")]
        Lop2 = 2,
        [Description("Lớp 3")]
        Lop3 = 3,
        [Description("Lớp 4")]
        Lop4 = 4,
        [Description("Lớp 5")]
        Lop5 = 5,
        [Description("Lớp 6")]
        Lop6 = 6,

        [Description("Lớp 7")]
        Lop7 = 7,
        [Description("Lớp 8")]
        Lop8 = 8,
        [Description("Lớp 9")]
        Lop9 = 9,
        [Description("Lớp 10")]
        Lop10 = 10,
        [Description("Lớp 11")]
        Lop11 = 11,
        [Description("Lớp 12")]
        Lop12 = 12,
        [Description("Ôn đại học")]
        OnDaiHoc = 13,
        [Description("Năng khiếu")]
        NangKhieu = 14,
        [Description("Ngoại ngữ")]
        NgoaiNgu = 15,
        [Description("Lớp khác")]
        LopKhac = 16,
        [Description("Hệ đại học")]
        HeDaiHoc = 17,
    }
    public enum EnumThoiGianDay
    {
        [Description("Thứ 2 - Sáng")]
        Thu2Sang = 1,
        [Description("Thứ 2 - Chiều")]
        Thu2Chieu = 2,
        [Description("Thứ 2 - Tối")]
        Thu2Toi = 3,
        [Description("Thứ 3 - Sang")]
        Thu3Sang = 4,
        [Description("Thứ 3 - Chiều")]
        Thu3Chieu = 5,
        [Description("Thứ 3 - Tối")]
        Thu3Toi = 6,
        [Description("Thứ 4 - Sáng")]
        Thu4Sang = 7,

        [Description("Thứ 4 - Chiều")]
        Thu4Chieu = 8,
        [Description("Thứ 4 - Tối")]
        Thu4Toi = 9,
        [Description("Thứ 5 - Sáng")]
        Thu5Sang = 10,
        [Description("Thứ 5 - Chiều")]
        Thu5Chieu = 11,
        [Description("Thứ 5 - Tối")]
        Thu5Toi = 12,
        [Description("Thứ 6 - Sáng")]
        Thu6Sang = 13,

        [Description("Thứ 6 - Chiều")]
        Thu6Chieu = 14,
        [Description("Thứ 6 - Tối")]
        Thu6Toi = 15,
        [Description("Thứ 7 - Sáng")]
        Thu7Sang = 16,
        [Description("Thứ 7 - Chiều")]
        Thu7Chieu = 17,
        [Description("Thứ 7 - Tối")]
        Thu7Toi = 18,
        [Description("CN - Sáng")]
        CNSang = 19,
        [Description("CN - Chiều")]
        CNChieu = 20,
        [Description("CN - Tối")]
        CNToi = 21,
    }
    public enum EnumNgheNghiep
    {
        [Description("Giáo viên")]
        GiaoVien = 1,
        [Description("Sinh viên")]
        SinhVien = 2,
        [Description("Đã tốt nghiệp")]
        DaTotNghiep = 3,
    }
    public enum EnumTrinhDo
    {
        [Description("Cao đẳng")]
        CaoDang = 1,
        [Description("Đại học")]
        DaiHoc = 2,
        [Description("Cử nhân")]
        CuNhan = 3,
        [Description("Thạc sỹ")]
        ThacSy = 4,
        [Description("Tiến sỹ")]
        TienSy = 5,
        [Description("Kỹ sư")]
        KySu = 6,
        [Description("Bằng cấp khác")]
        BangCapKhac = 7,
    }
    public enum EnumTinhThanh
    {
        [Description("An Giang")]
        AnGiang = 1,
        [Description("Bà rịa – Vũng tàu")]
        BaRiaVungTau = 2,
        [Description("Bắc Giang")]
        BacGiang = 3,
        [Description("Bắc Kạn")]
        BacKan = 4,
        [Description("Bạc Liêu")]
        BacLieu = 5,
        [Description("Bắc Ninh")]
        BacNinh = 6,
        [Description("Bến Tre")]
        BenTre = 7,
        [Description("Bình Định")]
        BinhDinh = 8,

        [Description("Bình Dương")]
        BinhDuong = 9,
        [Description("Bình Phước")]
        BinhPhuoc = 10,
        [Description("Bình Thuận")]
        BinhThuan = 11,
        [Description("Cà Mau")]
        CaMau = 12,
        [Description("Cần Thơ")]
        CanTho = 13,
        [Description("Cao Bằng")]
        CaoBang = 14,
        [Description("Đà Nẵng")]
        DaNang = 15,
        [Description("Đắk Lắk")]
        DakLak = 16,
        [Description("Đắk Nông")]
        DakNong = 17,

        [Description("Điện Biên")]
        DienBien = 18,
        [Description("Đồng Nai")]
        DongNai = 19,
        [Description("Đồng Tháp")]
        DongThap = 20,
        [Description("Gia Lai")]
        GiaLai = 21,
        [Description("Hà Giang")]
        HaGiang = 22,
        [Description("Hà Nam")]
        HaNam = 23,
        [Description("Hà Nội")]
        HaNoi = 24,
        [Description("Hà Tĩnh")]
        HaTinh = 25,

        [Description("Hải Dương")]
        HaiDuong = 26,
        [Description("Hải Phòng")]
        HaiPhong = 27,
        [Description("Hậu Giang")]
        HauGiang = 28,
        [Description("Hòa Bình")]
        HoaBinh = 29,
        [Description("Hưng Yên")]
        HungYen = 30,
        [Description("Khánh Hòa")]
        KhanhHoa = 31,
        [Description("Kiên Giang")]
        KienGiang = 32,
        [Description("Kon Tum")]
        KonTum = 33,

        [Description("Lai Châu")]
        LaiChau = 34,
        [Description("Lâm Đồng")]
        LamDong = 35,
        [Description("Lạng Sơn")]
        LangSon = 36,
        [Description("Lào Cai")]
        LaoCai = 37,
        [Description("Long An")]
        LongAn = 38,
        [Description("Nam Định")]
        NamDinh = 39,
        [Description("Nghệ An")]
        NgheAn = 40,
        [Description("Ninh Bình")]
        NinhBinh = 41,

        [Description("Ninh Thuận")]
        NinhThuan = 42,
        [Description("Phú Thọ")]
        PhuTho = 43,
        [Description("Phú Yên")]
        PhuYen = 44,
        [Description("Quảng Bình")]
        QuangBinh = 45,
        [Description("Quảng Nam")]
        QuangNam = 46,
        [Description("Quảng Ngãi")]
        QuangNgai = 47,
        [Description("Quảng Ninh")]
        QuangNinh = 48,
        [Description("Quảng Trị")]
        QuangTri = 49,

        [Description("Sóc Trăng")]
        SocTrang = 42,
        [Description("Sơn La")]
        SonLa = 43,
        [Description("Tây Ninh")]
        TayNinh = 44,
        [Description("Thái Bình")]
        ThaiBinh = 45,
        [Description("Thái Nguyên")]
        ThaiNguyen = 46,
        [Description("Thanh Hóa")]
        ThanhHoa = 47,
        [Description("Thừa Thiên Huế")]
        ThuaThienHue = 48,
        [Description("Tiền Giang")]
        TienGiang = 49,

        [Description("Thành phố Hồ Chí Minh")]
        ThanhPhoHoChiMinh = 50,
        [Description("Trà Vinh")]
        TraVinh = 51,
        [Description("Tuyên Quang")]
        TuyenQuang = 52,
        [Description("Vĩnh Long")]
        VinhLong = 53,
        [Description("Vĩnh Phúc")]
        VinhPhuc = 54,
        [Description("Yên Bái")]
        YenBai = 55,
    }
    public enum EnumGioiTinh
    {
        [Description("Nam")]
        Nam = 1,
        [Description("Nữ")]
        Nu = 2,
    }
    public enum EnumGiongNoi
    {
        [Description("Miền Nam")]
        MienNam = 1,
        [Description("Miền Trung")]
        MienTrung = 2,
        [Description("Miền Bắc")]
        MienBac = 3,
    }
    public class GiaSuViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Tỉnh")]
        public int? IdTinhThanh { get; set; }
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; }
        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; }
        [Display(Name = "Giới tính")]
        public string GioiTinhTex { get; set; }
        [Display(Name = "Giọng nói")]
        public int? GiongNoi { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }
        [Display(Name = "Nơi sinh")]
        public int? NoiSinh { get; set; }
        [Display(Name = "Số CMND")]
        public int? SoCMND { get; set; }
        [Display(Name = "Nguyên quán")]
        public int? NguyenQuan { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Điện thoại")]
        public int? DienThoai { get; set; }
        [Display(Name = "Ảnh thẻ")]
        public string AnhThe { get; set; }
        [Display(Name = "Ảnh bằng cấp")]
        public string AnhBangCap { get; set; }
        [Display(Name = "Trường")]
        public string Truong { get; set; }
        [Display(Name = "Ngành học")]
        public string NganhHoc { get; set; }
        [Display(Name = "Năm tốt nghiệp")]
        public int? NamTotNghiep { get; set; }
        [Display(Name = "Nghề nghiệp")]
        public int? NgheNghiep { get; set; }
        [Display(Name = "Trình độ")]
        public int? TrinhDo { get; set; }
        [Display(Name = "Ưu điểm")]
        public string UuDiem { get; set; }
        [Display(Name = "Môn dạy")]
        public string MonDay { get; set; }
        [Display(Name = "Lớp dạy")]
        public string LopDay { get; set; }
        [Display(Name = "Khu vực dạy")]
        public string QuanHuyen { get; set; }
        [Display(Name = "Thời gian dạy")]
        public string ThoiGianDay { get; set; }
        [Display(Name = "Số buổi dạy")]
        public int? SoBuoiDay { get; set; }
        [Display(Name = "Mức lương yêu cầu")]
        public int? MucLuongYeuCau { get; set; }
        [Display(Name = "Yêu cầu khác")]
        public string YeuCauKhac { get; set; }
    }
}