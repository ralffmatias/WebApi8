if object_id(N'dbo.p_inserir_livro', N'P') is not null
begin
	drop procedure dbo.p_inserir_livro
end
GO

create procedure dbo.p_inserir_livro
	(
		@nm_titulo		varchar(150),
		@id_autor		int,
		@id_livro		int = null output,
		@cd_retorno		int output,
		@nm_retorno		varchar(150) output
	)
as

/*

	begin tran
		declare		@nm_titulo		varchar(150) = 'Nome Teste',
					@id_autor		int = 1,
					@id_livro		int = null,
					@cd_retorno		int,
					@nm_retorno		varchar(150)

		exec dbo.p_inserir_livro
			@nm_titulo		= @nm_titulo,
			@id_autor		= @id_autor,
			@id_livro		= @id_livro output,
			@cd_retorno		= @cd_retorno output,
			@nm_retorno		= @nm_retorno output	
			
		select	id_livro	= @id_livro, 
				cd_retorno	= @cd_retorno, 
				nm_retorno	= @nm_retorno

	rollback
*/

set nocount on
begin try

	declare @inserted table (id int)

	if not exists	(	select	top 1 1 
						from	dbo.livros 
						where	nm_titulo = @nm_titulo
						and		id_autor = @id_autor
					)
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


		insert into dbo.livros (nm_titulo, id_autor)
		output inserted.id_livro into @inserted(id)
		values (@nm_titulo, @id_autor)

		select @id_livro = id from @inserted
		delete from @inserted

		if @id_livro is null
		begin
			
			select @cd_retorno = 1,
					@nm_retorno = 'Livro não inserido, procure o time tecnico.'
			return
		end

		select @cd_retorno = 0,
				@nm_retorno = 'Processamento efetuado com sucesso'
		return
	end

	select @cd_retorno = 1,
			@nm_retorno = 'Esse livro já está registrado.'
	return
	
end try
begin catch

	select @cd_retorno = 1,
			@nm_retorno = ERROR_MESSAGE()

end catch

