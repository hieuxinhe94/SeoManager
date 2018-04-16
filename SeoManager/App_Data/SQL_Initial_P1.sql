
-- /*
--	Khởi tạo stored để đọc file từ vựng 
-- */
 
--CREATE PROC [dbo].[ns_txt_file_read]  
--    @os_file_name NVARCHAR(256) 
--   ,@text_file VARCHAR(MAX) OUTPUT  
--AS  
--DECLARE @sql NVARCHAR(MAX) 
--      , @parmsdeclare NVARCHAR(4000)  

--SET NOCOUNT ON  

--SET @sql = 'select @text_file=(select * from openrowset ( 
--           bulk ''' + @os_file_name + ''' 
--           ,SINGLE_CLOB) x 
--           )' 

--SET @parmsdeclare = '@text_file varchar(max) OUTPUT'  

--EXEC sp_executesql @stmt = @sql 
--                 , @params = @parmsdeclare 
--                 , @text_file = @text_file OUTPUT  ;


-- bo qua dem tung dong insert
SET NOCOUNT ON;
DECLARE @date Datetime2 = GETDATE();
/*
	Khởi tạo 3 vai trò của hệ thống
*/
	-- HACK ID
	INSERT INTO VaiTro(Id,Ten)
	VALUES
	(1,N'Adminstrator'),
	(2,N'Cộng tác viên'),
	(3,N'Người dùng');
 
-----------------------------------------
/*
	Khởi tạo người dùng mặc định quyền admin với ID = 1
*/
-- HACK ID
	INSERT INTO NguoiDung(Ten,Password,VaiTroId,Email, NgayKichHoat, TrangThai
		   ,[PercentOfProfileInfo]
           ,[MoneyInAccount]
           ,[DomainAssign]
           ,[PercentOfDocumentViewer])
	VALUES
	(N'System Manager','12345678', 1, N'admin@seomanager.com',GETDATE(),1,100,100000,100000,100);
	 
--------------------
-- Khởi tạo table chứa các từ vựng random
DECLARE @wordTable TABLE (id int identity(1,1), _text varchar(100) null);
-- chứa nội dung đọc từ file
DECLARE @w VARCHAR(MAX) 
-- đọc file vào biến
EXEC ns_txt_file_read 'E:\FREELANCER\TheAnhDhv\resources\words_alpha.txt', @w output 
-- đưa một lượng cần thiết vào csdl : 100 word 
INSERT INTO @wordTable (_text)
SELECT top 100 CAST( cs.value as varchar(10)) as result from string_split(@w,char(10)) cs ORDER BY NEWID()  ; 

/*
	Khởi tạo hàng ngàn domain có thể dò được vào bảng Domain
	Người dùng sẻ tìm thấy domain của họ có thể được xử lý bởi hệ thống hay không
*/
 
------------
DECLARE @i INT = 1;

While(@i<=100)
BEGIN
	-- Mặc định một domain chưa được cấp phát cho người dùng thì sẻ thuộc quyền sở hữu là ADMIN và NguoiDungId = 1
	DECLARE @tmp1 varchar(1000) = CAST(@i AS varchar(1000));
	INSERT INTO Domain([URL],Ten,NguoiDungId,MieuTa,NgayKichHoat, TrangThai)
	VALUES(N'https://wwww.domain'+ @tmp1 + '.com' ,N' Domain' + @tmp1 + '.com',1,N'Đây là miêu tả của domain' + @tmp1 + '.com',@date,1);
	
	--	Khởi tạo hàng ngàn link có thể dò được vào bảng link cho mỗi domain được tạo ra
	DECLARE @k INT =1;
	While(@k<=100)
	BEGIN
			 DECLARE @tmp2 varchar(1000) = CAST(@k AS varchar(1000));
			 INSERT INTO Link(DomainId,MoTa,TieuDe, NgayTao,TrangThai)
			 VALUES(@i,'https://wwww.domain'+ @tmp1 + '.com/post/' + @tmp2 + '.html' ,N'Title of link' + @tmp2,@date,1);
			-- Trong mỗi post insert khoảng 10 từ khóa có chứa đường link đi nơi khác
			 DECLARE @t INT = 1;
			 While(@t<=10)
			 BEGIN
				DECLARE @tmp3 varchar(1000) = CAST(@t AS varchar(1000));
				-- WARRING: Word : từ khóa thì là ngẫu nhiên, được lấy từ trong từ điễn, nhưng hiện tại làm cách này sẻ dễ quan sát hơn
				-- Chạy stored bên trên để thay thế đoạn này nếu cảm thấy cần thiết
				INSERT INTO Word([Text])
				(select top 1 _text from @wordTable where id = RAND(99));
			    
				---- Tiếp tục insert vào bảng ràng buộc giữa Link Và word
				DECLARE @tmpId int;
				SET @tmpId= 1  ;
				  
					INSERT INTO LinkAndWord(LinkId, WordId,XepHang,NgayCapNhat, TrangThai )
					VALUES(@i,@tmpId,RAND(10),@date,1);
					
					INSERT INTO BackLinkAndWord(LinkId,LinkToId, WordId,NgayCapNhat, TrangThai )
					VALUES(@i,RAND(999),@tmpId,@date,1);		 

				 -- increase count variable
				SET @t = @t+1;
		     END

		 -- increase count variable
		 SET @k = @k+1;
	END

	-- increase count variable
	SET @i = @i+1;
END
------------------


---//////////////////////////////////////
-- Check status of records 
-- in message
SET NOCOUNT OFF;
	--- check again
	DECLARE @statusOfSuccess01 INT = 0;
	SELECT @statusOfSuccess01 = COUNT(*)  FROM VaiTro ;
	IF(@statusOfSuccess01 != 3)
	BEGIN
	PRINT N' ===========ERROR============'
	END
	 	--- check again
	DECLARE @statusOfSuccess02 INT = 0;
	SELECT @statusOfSuccess02 = ID  FROM NguoiDung WHERE Email= N'admin@seomanager.com';
	IF(@statusOfSuccess02 != 1)
	BEGIN
	PRINT N' ===========ERROR============'
	END
 	 	--- check again
	DECLARE @statusOfSuccess03 INT = 0;
	SELECT @statusOfSuccess03 = ID  FROM Domain;
	IF(@statusOfSuccess03 > 1)
	BEGIN
	PRINT N'Domain records :' + CAST(@statusOfSuccess03 AS NVARCHAR(1000));
	END

	 	--- check again
	DECLARE @statusOfSuccess04 INT = 0;
	SELECT @statusOfSuccess04 = ID  FROM Word;
	IF(@statusOfSuccess04 > 1)
	BEGIN
	PRINT N'Word records :' + CAST(@statusOfSuccess04 AS NVARCHAR(1000));
	END

		 	--- check again
	DECLARE @statusOfSuccess05 INT = 0;
	SELECT @statusOfSuccess05 = ID  FROM Link;
	IF(@statusOfSuccess05 > 1)
	BEGIN
	PRINT N'Link records :' + CAST(@statusOfSuccess05 AS NVARCHAR(1000));
	END

			 	--- check again
	DECLARE @statusOfSuccess06 INT = 0;
	SELECT @statusOfSuccess06 = ID  FROM LinkAndWord;
	IF(@statusOfSuccess06 > 1)
	BEGIN
	PRINT N'LinkAndWord records :' + CAST(@statusOfSuccess06 AS NVARCHAR(1000));
	END

				 	--- check again
	DECLARE @statusOfSuccess07 INT = 0;
	SELECT @statusOfSuccess07 = ID  FROM BackLinkAndWord;
	IF(@statusOfSuccess07 > 1)
	BEGIN
	PRINT N'BackLinkAndWord records :' + CAST(@statusOfSuccess07 AS NVARCHAR(1000));
	END
