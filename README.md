LMS-Library.Api

Giới thiệu

LMS-Library.Api là một API quản lý thư viện được xây dựng bằng .NET Core. Dự án hỗ trợ các chức năng như quản lý sách, mượn/trả sách, và quản lý người dùng.

Yêu cầu hệ thống

.NET SDK 6.0 hoặc mới hơn

SQL Server hoặc PostgreSQL (tùy theo cấu hình cơ sở dữ liệu)

Công cụ quản lý API như Postman để kiểm tra API

Cài đặt và chạy dự án

1. Sao chép kho lưu trữ

git clone https://github.com/pthien24/LMS-Library.Api.git
cd LMS-Library.Api

2. Cài đặt các package cần thiết

dotnet restore

3. Cấu hình cơ sở dữ liệu

Cập nhật chuỗi kết nối trong appsettings.json hoặc appsettings.Development.json

Chạy lệnh để áp dụng migrations:

dotnet ef database update

4. Chạy ứng dụng

dotnet run

Ứng dụng sẽ chạy trên http://localhost:5000 hoặc https://localhost:5001.

Các API chính

GET /books - Lấy danh sách sách

POST /books - Thêm sách mới

PUT /books/{id} - Cập nhật sách

DELETE /books/{id} - Xóa sách

Đóng góp

Mọi đóng góp đều được hoan nghênh! Vui lòng tạo Pull Request hoặc mở Issue nếu bạn có đề xuất.

Giấy phép

Dự án này được phát hành theo giấy phép MIT.
