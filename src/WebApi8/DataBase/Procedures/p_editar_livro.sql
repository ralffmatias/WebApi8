if object_id(N'dbo.p_editar_livro', N'P') is not null
begin
	drop procedure dbo.p_editar_livro
end
GO

create procedure dbo.p_editar_livro
	(
		@nm_titulo		varchar(150) = null,
		@id_autor		int = null,
		@id_livro		int,
		@cd_retorno		int output,
		@nm_retorno		varchar(150) output
	)
as

/*

	begin tran
		declare		@nm_titulo		varchar(150) = 'Nome Teste auterado',
					@id_autor		int = 1,
					@id_livro		int = 6,
					@cd_retorno		int,
					@nm_retorno		varchar(150)

		exec dbo.p_editar_livro
			@nm_titulo		= @nm_titulo,
			@id_autor		= @id_autor,
			@id_livro		= @id_livro,
			@cd_retorno		= @cd_retorno output,
			@nm_retorno		= @nm_retorno output	
			
		select	cd_retorno	= @cd_retorno, 
				nm_retorno	= @nm_retorno

	rollback
*/

set nocount on
begin try

	if exists	(
					select	top 1 1 
					from	dbo.livros 
					where	id_livro = @id_livro
				)
	begin

		if @id_autor is not null
		begin
			if not exists	(	select top 1 1
								from	dbo.autores
								where	id_autor = @id_autor
							)
			begin
				select @cd_retorno = 1,
						@nm_retorno = 'O autor informado não existe no banco de dados.'

				return
			end
		end


		update	l 
		set		nm_titulo	= isnull(@nm_titulo, nm_titulo),
				id_autor	= isnull(@id_autor, id_autor)
		from	dbo.livros l
		where	id_livro = @id_livro

		select @cd_retorno = 0,
				@nm_retorno = 'Processamento efetuado com sucesso'
		return
	end

	select @cd_retorno = 1,
			@nm_retorno = 'id_livro, informado não encontrado.'
	return
	
end try
begin catch

	select @cd_retorno = 1,
			@nm_retorno = ERROR_MESSAGE()

end catch

