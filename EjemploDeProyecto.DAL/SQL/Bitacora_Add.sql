CREATE or ALTER proc [dbo].[Bitacora_Add]	@tipo int, @usuario varchar(50), @mensaje varchar(max) as

insert Bitacora values (getdate(), @tipo, @usuario, @mensaje)