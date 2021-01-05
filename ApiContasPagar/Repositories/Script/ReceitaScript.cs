namespace ApiContasPagar.Repositories.Script
{
    public class ReceitaScript
    {
        public static string Insert { get => @"
            INSERT INTO Receita VALUES(
                @Descricao,
                @Valor, 
                GETDATE(), 
                @recebido)"; }
        public static string Get { get => @"
            SELECT	ID AS Id,
		            DESCRICAO AS Descricao,
		            VALOR AS Valor,
		            DATA_RECEITA AS Data,
                    CAST(CASE WHEN RECEBIDO = 'N' THEN 0 ELSE 1 END AS BIT) AS Recebido
            FROM Receita WHERE ID = @Id"; }

        public static string Update { get => @"
            UPDATE Receita
            SET Descricao = @Descricao,
                Valor = @Valor,
	            Recebido = @Recebido
            WHERE Id = @Id
            
            SELECT	ID AS Id,
		            DESCRICAO AS Descricao,
		            VALOR AS Valor,
		            DATA_RECEITA AS Data,
                    CAST(CASE WHEN RECEBIDO = 'N' THEN 0 ELSE 1 END AS BIT) AS Recebido
            FROM    Receita
            WHERE ID = @Id"; }

        public static string Delete { get => @"
            DELETE FROM Receita
            WHERE ID = @id"; }

        public static string GetAll { get => @"
            SELECT	ID AS Id,
		            DESCRICAO AS Descricao,
		            VALOR AS Valor,
		            DATA_RECEITA AS Data,
		            CAST(CASE WHEN RECEBIDO = 'N' THEN 0 ELSE 1 END AS BIT) AS Recebido
            FROM Receita 
            ORDER BY Id, Data
            OFFSET @Offset ROWS
			FETCH NEXT @PageSize ROWS ONLY"; }

        public static string GetAllTotalCount { get => @"
            SELECT COUNT(*)
			FROM Receita"; }
    }
}