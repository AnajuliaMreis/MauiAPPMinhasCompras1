using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MauiAPPMinhasCompras.Models
{
    public class Produto
    {
        private string _descricao;
        private double _quantidade;
        private double _preco;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Por favor, preencha a descrição");

                _descricao = value;
            }
        }

        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value <= 0)
                    throw new Exception("Quantidade deve ser maior que 0");

                _quantidade = value;
            }
        }

        public double Preco
        {
            get => _preco;
            set
            {
                if (value <= 0)
                    throw new Exception("Preço deve ser maior que 0");

                _preco = value;
            }
        }

        public string Categoria { 
            get; 
            set;
        }

        public double Total => Quantidade * Preco;
    }
}
