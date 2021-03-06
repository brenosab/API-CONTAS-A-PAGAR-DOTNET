﻿namespace ApiContasPagar.Repositorio.Script
{
    public class DespesaScript
    {
        public static string Insert { get => @"
            INSERT INTO Despesa VALUES(
                @Descricao,
                @Valor, 
                GETDATE(), 
                @pago)"; }
        public static string Get { get => @"
            SELECT	ID AS Id,
		            DESCRICAO AS Descricao,
		            VALOR AS Valor,
		            DATA_DESPESA AS Data,
                    CAST(CASE WHEN PAGO = 'N' THEN 0 ELSE 1 END AS BIT) AS Pago
            FROM DESPESA WHERE ID = @Id"; }

        public static string Update { get => @"
            UPDATE Despesa
            SET Descricao = @Descricao,
                Valor = @Valor,
	            Pago = @Pago
            WHERE Id = @Id
            
            SELECT	ID AS Id,
		            DESCRICAO AS Descricao,
		            VALOR AS Valor,
		            DATA_DESPESA AS Data,
                    CAST(CASE WHEN PAGO = 'N' THEN 0 ELSE 1 END AS BIT) AS Pago
            FROM    Despesa
            WHERE ID = @Id"; }

        public static string Delete { get => @"
            DELETE FROM Despesa
            WHERE ID = @id"; }

        public static string GetAll { get => @"
            SELECT	ID AS Id,
		            DESCRICAO AS Descricao,
		            VALOR AS Valor,
		            DATA_DESPESA AS Data,
		            CAST(CASE WHEN PAGO = 'N' THEN 0 ELSE 1 END AS BIT) AS Pago
            FROM DESPESA 
            ORDER BY Id, Data
            OFFSET @Offset ROWS
			FETCH NEXT @PageSize ROWS ONLY"; }

        public static string GetAllTotalCount { get => @"
            SELECT COUNT(*)
			FROM Despesa"; }
    }
}