if object_id(N'dbo.p_excluir_livro', N'P') is not null
begin
	drop procedure dbo.p_excluir_livro
end
GO

create procedure dbo.p_excluir_livro
	(
		@id_livro		int,
		@cd_retorno		int output,
		@nm_retorno		varchar(150) output
	)
as

/*

	begin tran
		declare		@id_livro		int = 2,
					@cd_retorno		int,
					@nm_retorno		varchar(150)

		exec dbo.p_excluir_livro
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

		delete 
		from	dbo.livros
		where	id_livro = @id_livro

		select	@cd_retorno = 0,
				@nm_retorno = 'Processamento efetuado com sucesso'
		return
	end

	select @cd_retorno = 1,
			@nm_retorno = 'id_livro, informado não encontrado.'
	return
	
end try
begin catch

	select	@cd_retorno = 1,
			@nm_retorno = ERROR_MESSAGE()

end catch

