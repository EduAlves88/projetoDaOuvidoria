using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
namespace projetoDaOuvidoria.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base("Manifesto")
        {
        }
        public DbSet<Manifesto> Manifesto { get; set; }
    }
}


//variavel de conecao com banco por meio de String

/* public string conec = "SERVER=localhost; DASABASE=ouvidoriaProva.Models.Contexto; UID = ; PWD = ; PORT = ;";
 public MySqlConnection con = null;

 public void AbrirCon()
 {
     try
     {
         con = new MySqlConnection(conec);
         con.Open();
         HttpContext.Current.Response.Write("Conectado!");
     }
     catch (Exception ex)
     {
         HttpContext.Current.Response.Write("Erro ao conectar!" + ex);
     }
 }*/