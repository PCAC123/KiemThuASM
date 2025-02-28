-- Chọn cơ sở dữ liệu để sử dụng
USE demoQLTV;

-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(15)
);

-- Tạo bảng KHOA
CREATE TABLE KHOA (
    MaKhoa INT IDENTITY(1,1) PRIMARY KEY,
    TenKhoa NVARCHAR(100)
);

-- Tạo bảng Lop
CREATE TABLE Lop (
    MaLop INT PRIMARY KEY IDENTITY(1,1),
    TenLop NVARCHAR(100)
);

-- Tạo bảng DOCGIA
CREATE TABLE DOCGIA (
    MaDG INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(100),
    MaLop INT,
    MaKhoa INT,
    FOREIGN KEY (MaKhoa) REFERENCES KHOA(MaKhoa),
    FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
);

-- Tạo bảng NHAXUATBAN
CREATE TABLE NHAXUATBAN (
    MaNXB INT IDENTITY(1,1) PRIMARY KEY,
    TenNXB NVARCHAR(100),
    DiaChi NVARCHAR(200),
    NgayThanhLap DATE
);

-- Tạo bảng THELOAI
CREATE TABLE THELOAI (
    MaTheLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenTheLoai NVARCHAR(50)
);

-- Tạo bảng SACH
CREATE TABLE SACH (
    MaSach INT PRIMARY KEY IDENTITY(1,1),
    TenSach NVARCHAR(200),
    MaTheLoai INT,
    SoLuong INT CHECK (SoLuong >= 1),
    MaNXB INT,
    NamXB INT,
    FOREIGN KEY (MaNXB) REFERENCES NHAXUATBAN(MaNXB),
    FOREIGN KEY (MaTheLoai) REFERENCES THELOAI(MaTheLoai)
);

-- Tạo bảng PHIEUMUON
CREATE TABLE PHIEUMUON (
    MaPhieuMuon INT PRIMARY KEY IDENTITY(1,1),
    MaDG INT,
    MaNV INT,
    NgayMuon DATE,
    NgayTra DATE,
    FOREIGN KEY (MaDG) REFERENCES DOCGIA(MaDG),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- Tạo bảng CHITIETPHIEUMUON
CREATE TABLE CHITIETPHIEUMUON (
    MaPhieuMuon INT,
    MaSach INT,
    NgayMuon DATE,
    NgayTra DATE,
    PRIMARY KEY (MaPhieuMuon, MaSach),
    FOREIGN KEY (MaPhieuMuon) REFERENCES PHIEUMUON(MaPhieuMuon),
    FOREIGN KEY (MaSach) REFERENCES SACH(MaSach)
);

-- Tạo bảng roles
CREATE TABLE roles (
    role_id INT PRIMARY KEY IDENTITY(1,1),
    role_name NVARCHAR(50) NOT NULL
);

-- Tạo bảng user_roles
CREATE TABLE user_roles (
    username NVARCHAR(50),
    password NVARCHAR(255) NOT NULL,
    role_id INT,
    MaNV INT,
    FOREIGN KEY (role_id) REFERENCES roles(role_id),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
	PRIMARY KEY (username, role_id)
);

-- Thêm dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (HoTen, NgaySinh, DiaChi, DienThoai) VALUES
(N'Nguyễn Văn A', '1980-01-01', N'123 Đường ABC, Quận 1, TP HCM', '0909123456'),
(N'Trần Thị B', '1985-02-02', N'456 Đường DEF, Quận 2, TP HCM', '0912345678'),
(N'Lê Văn C', '1990-03-03', N'789 Đường GHI, Quận 3, TP HCM', '0923456789');

-- Thêm dữ liệu vào bảng KHOA
INSERT INTO KHOA (TenKhoa) VALUES
(N'Công Nghệ Thông Tin'),
(N'Toán Học'),
(N'Vật Lý'),
(N'Kinh tế'),
(N'Ngoại ngữ'),
(N'Khoa học Xã hội và Nhân văn'),
(N'Khoa học Tự nhiên'),
(N'Y học và Khoa học Sức khỏe'),
(N'Luật'),
(N'Nông nghiệp và Lâm nghiệp'),
(N'Du lịch và Quản lý Nhà hàng - Khách sạn');

-- Thêm dữ liệu vào bảng Lop
INSERT INTO Lop (TenLop) VALUES
(N'Lop 1'),
(N'Lop 2'),
(N'Lop 3');

-- Thêm dữ liệu vào bảng DOCGIA
INSERT INTO DOCGIA (HoTen, NgaySinh, GioiTinh, DiaChi, MaLop, MaKhoa) VALUES
(N'Pham Van D', '2000-04-04', N'Nam', N'123 Đường ABC, Quận 1, TP HCM', 1, 1),
(N'Hoang Thi E', '2001-05-05', N'Nu', N'456 Đường DEF, Quận 2, TP HCM', 2, 2),
(N'Vu Van F', '2002-06-06', N'Nam', N'789 Đường GHI, Quận 3, TP HCM', 3, 3);

-- Thêm dữ liệu vào bảng NHAXUATBAN
INSERT INTO NHAXUATBAN (TenNXB, DiaChi, NgayThanhLap) VALUES
(N'NXB Tre', N'123 Đường XYZ, TP HCM', '1990-01-01'),
(N'NXB Kim Dong', N'456 Đường ABC, TP HCM', '1995-02-02'),
(N'NXB Giao Duc', N'789 Đường DEF, TP HCM', '2000-03-03'),
(N'NXB Giáo Dục', N'123 Nguyen Trai, Ha Noi', '1995-06-15'),
(N'NXB Kim Dong', N'456 Le Lai, Ho Chi Minh', '2000-01-01');

-- Thêm dữ liệu vào bảng THELOAI
INSERT INTO THELOAI (TenTheLoai) VALUES
(N'Khoa học'),
(N'Văn học'),
(N'Kinh tế'),
(N'Lịch sử'),
(N'Chính trị'),
(N'Triết học'),
(N'Văn học hiện thực'),
(N'Tâm lý học'),
(N'Tiểu sử'),
(N'Tự truyện'),
(N'Hồi ký'),
(N'Kỹ thuật ô tô'),
(N'Xây dựng'),
(N'Cơ khí'),
(N'Điện tử'),
(N'Yoga'),
(N'Sức khỏe tinh thần'),
(N'Thể dục'),
(N'Dinh dưỡng'),
(N'Tiểu thuyết giả tưởng (fantasy)'),
(N'Tiểu thuyết tình cảm'),
(N'Tiểu thuyết hài hước'),
(N'Tiểu thuyết xã hội'),
(N'Tiểu thuyết lịch sử'),
(N'Tiểu thuyết trinh thám'),
(N'Tiểu thuyết phiêu lưu'),
(N'Tiểu thuyết kinh dị'),
(N'Tiểu thuyết khoa học viễn tưởng'),
(N'Tiểu thuyết lãng mạn');

-- Thêm dữ liệu vào bảng SACH
INSERT INTO SACH (TenSach, MaTheLoai, SoLuong, MaNXB, NamXB) VALUES
(N'Sach Van Hoc 1', 1, 10, 1, 2010),
(N'Sach Khoa Hoc 1', 2, 20, 2, 2015),
(N'Sach Toan Hoc 1', 3, 15, 3, 2020),
(N'Cơ sở dữ liệu', 1, 5, 1, 2020),
(N'Đắc Nhân Tâm', 2, 10, 2, 2018);

-- Thêm dữ liệu vào bảng PHIEUMUON
INSERT INTO PHIEUMUON (MaDG, MaNV, NgayMuon, NgayTra) VALUES
(1, 1, '2023-01-01', '2023-01-10'),
(2, 2, '2023-02-01', '2023-02-10'),
(3, 3, '2023-03-01', '2023-03-10');

-- Thêm dữ liệu vào bảng CHITIETPHIEUMUON
INSERT INTO CHITIETPHIEUMUON (MaPhieuMuon, MaSach, NgayMuon, NgayTra) VALUES
(1, 1, '2023-01-01', '2023-01-10'),
(2, 2, '2023-02-01', '2023-02-10'),
(3, 3, '2023-03-01', '2023-03-10');

-- Thêm dữ liệu vào bảng roles
INSERT INTO roles (role_name) VALUES
('ThuThu'),
('DocGia');

-- Thêm dữ liệu vào bảng user_roles
INSERT INTO user_roles (username, password, role_id, MaNV) VALUES
('user1', 'us123', 1, 1),
('user2', 'us456', 2, 2),
('user3', 'us789', 2, 3);

