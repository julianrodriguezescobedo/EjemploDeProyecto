ALTER proc [dbo].[Bitacora_GetAllFiltered] @total bit, @page int, @perPage int, @usuario nvarchar(50), @desde datetime, @hasta datetime, @tipo int, @like nvarchar(255) as
begin
	if @desde is null select @desde = getdate()
	if @hasta is null select @hasta = getdate()
	
	select @like = '%' + isnull(@like,'') + '%'

	select @page = @page -1

	if @total = 1
	begin
		select	'Page' = @page + 1,
				'PerPage' = @perPage,
				'Total' = count(*),
				'TotalPages' = ceiling(convert(float,count(*)) / convert(float,@perPage))
		from	Bitacora a 
		where	(a.Usuario = @usuario or @usuario is null)
				and convert(date,a.Fecha) between convert(date,@desde) and convert(date,@hasta)
				and (a.Tipo = @tipo or @tipo = 3 or @tipo is null)
				and 
				(
					a.Usuario like @like
					or a.Mensaje like @like
				)
	end
	else
	begin
		select	*
		from
		(
			select	ROW_NUMBER() OVER (order by a.Fecha) as [Index], 
					a.Id,
					a.Usuario,
					a.Fecha,
					a.Tipo,
					a.Mensaje
			from	Bitacora a 
			where	(a.Usuario = @usuario or @usuario is null)
					and convert(date,a.Fecha) between convert(date,@desde) and convert(date,@hasta)
					and (a.Tipo = @tipo or @tipo = 3 or @tipo is null)
					and 
					(
						a.Usuario like @like
						or a.Mensaje like @like
					)
		) z
		where z.[Index] between (@perPage * @page) + 1 AND (@perPage * @page) + @perPage
	end
end