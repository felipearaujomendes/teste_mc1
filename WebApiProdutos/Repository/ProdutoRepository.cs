using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiProdutos.Model;

namespace WebApiProdutos.Repository
{
    public class ProdutoRepository
    {
        string conString = "";

        public ProdutoRepository(IConfiguration configuration)
        {
            conString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Produtos> ListaProdutos()
        {
            using (var conexaoBD = new SqlConnection(conString))
            {

                var resultadoDados = conexaoBD.Query<Produtos>
                (
                  @"
                    SELECT Name,Sku,Locality,inv.Quantity,Type,IsMarketable FROM Warehouse as wa
                    INNER JOIN Inventory as inv
                    ON wa.Id = inv.Id
                    INNER JOIN Produto as prod
                    ON inv.Id = prod.FK_Inventory
                    "
                ).ToList();
                return resultadoDados;
            }
        }

        public Produtos ProdutoPorId(long sku)
        {
            using (var conexaoBD = new SqlConnection(conString))
            {

                var resultadoDados = conexaoBD.Query<Produtos>
               (
                 @"
                    SELECT Name,Sku,Locality,inv.Quantity,Type,IsMarketable FROM Warehouse as wa
                    INNER JOIN Inventory as inv
                    ON wa.Id = inv.Id
                    INNER JOIN Produto as prod
                    ON inv.Id = prod.FK_Inventory
                    WHERE Sku = " + sku
               ).SingleOrDefault();

                return resultadoDados;
            }
        }

        public void InsereProduto(Produto produto)
        {
            using (var conexaoBD = new SqlConnection(conString))
            {
                #region Warehouse

                var wareHouseData = new Warehouse()
                {
                    Locality = produto.inventory.Warehouses[0].Locality,
                    Quantity = produto.inventory.Warehouses[0].Quantity,
                    Type = produto.inventory.Warehouses[0].Type
                };
                conexaoBD.Execute(@"insert Warehouse(Locality, Quantity,Type)
                                    values (@Locality, @Quantity,@Type)", wareHouseData);
                #endregion


                #region Inventory

                var retornaIdWarehouse = conexaoBD.Query<Warehouse>
                (
                  "select TOP 1 Id  from Warehouse ORDER BY Id DESC"
                ).Single();

                var inventoryData = new Inventory()
                {
                    Quantity = produto.inventory.Quantity,
                    FK_IdWarehouse = retornaIdWarehouse.Id
                };

                conexaoBD.Execute(@"insert Inventory(Quantity,FK_IdWarehouse)
                                   values (@Quantity,@FK_IdWarehouse)", inventoryData);
                #endregion

                #region Produto

                var retornaIdInventory = conexaoBD.Query<Inventory>
                 (
                   "select TOP 1 Id  from Inventory ORDER BY Id DESC"
                 ).Single();


                var produtoData = new Produto()
                {
                    Name = produto.Name,
                    IsMarketable = produto.IsMarketable,
                    FK_Inventory = retornaIdInventory.Id
                };

                conexaoBD.Execute(@"insert Produto(Name,IsMarketable,FK_Inventory)
                                   values (@Name,@IsMarketable,@FK_Inventory)", produtoData);
                #endregion

            }
        }

        public void DeletaProduto(long sku)
        {
            using (var conexaoBD = new SqlConnection(conString))
            {
                conexaoBD.Execute(@"DELETE FROM Produto WHERE Sku = " + sku);
            }
        }
    }
}
