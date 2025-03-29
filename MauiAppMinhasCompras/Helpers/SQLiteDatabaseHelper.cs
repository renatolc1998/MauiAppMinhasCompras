﻿using MauiAppMinhasCompras.Models;
using SQLite;
 
namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }
        
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table < Produto >().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }

        public Task<List<Produto>> SearchCategoria(string q, string r)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%' AND categoria LIKE '%" + r + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }

        public Task<List<RelatorioCategoria>> GetTotalPorCategoria()
        {
            string sql = "SELECT Categoria, SUM(Quantidade * Preco) AS TotalGasto FROM Produto GROUP BY Categoria";
            return _conn.QueryAsync<RelatorioCategoria>(sql);
        }
    }
    public class RelatorioCategoria
    {
        public string Categoria { get; set; }
        public double TotalGasto { get; set; }
    }

}
