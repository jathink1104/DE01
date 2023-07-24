/*
CREATED		7/24/2023
MODIFIED		7/24/2023
PROJECT		
MODEL			
COMPANY		
AUTHOR		
VERSION		
DATABASE		MS SQL 2005 
*/
create database QuanlySV
go
use QuanlySV
go


DROP TABLE [SINHVIEN] 
GO
DROP TABLE [LOP] 
GO


CREATE TABLE [LOP]
(
	[MALOP] CHAR(3) NOT NULL,
	[TENLOP] NVARCHAR(30) NOT NULL,
PRIMARY KEY ([MALOP])
) 
GO

CREATE TABLE [SINHVIEN]
(
	[MASV] CHAR(6) NOT NULL,
	[HOTENSV] NVARCHAR(40) NULL,
	[MALOP] CHAR(3) NOT NULL,
PRIMARY KEY ([MASV])
) 
GO


ALTER TABLE [SINHVIEN] ADD  FOREIGN KEY([MALOP]) REFERENCES [LOP] ([MALOP])  ON UPDATE NO ACTION ON DELETE NO ACTION 
GO


SET QUOTED_IDENTIFIER ON
GO


SET QUOTED_IDENTIFIER OFF
GO
alter table SINHVIEN
add NGAYSINH datetime

