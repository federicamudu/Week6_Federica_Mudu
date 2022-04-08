using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StazionePolizia
{
    public class RepositoryPoliziaDB : IRepositoryPolizia
    {
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProvaAgenti;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void AgentiArea(string area)
        {
            using (SqlConnection connessione = new SqlConnection(connectionString))
            {
                connessione.Open();
                SqlCommand comando = new SqlCommand("select * from Agente where AreaGeografica=@AreaGeografica", connessione);
                comando.Parameters.AddWithValue("@AreaGeografica", area);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    var nome = (string)reader["Nome"];
                    var cognome = (string)reader["Cognome"];
                    var codiceFiscale = (string)reader["CodiceFiscale"];
                    var annoDiInizioAttivita = (int)reader["AnnoDiInizioAttivita"];
                    Agente agente = new Agente(nome, cognome, codiceFiscale, area, annoDiInizioAttivita);
                    Console.WriteLine(agente.StampaDati());
                }
                connessione.Close();
            }
        }

        public void AgentiConAnniDiServizio(int anni)
        {
            using (SqlConnection connessione = new SqlConnection(connectionString))
            {
                connessione.Open();
                int annoDiInizioAttivita = DateTime.Today.Year - anni;
                SqlCommand comando = new SqlCommand("select * from Agente where AnnoDiInizioAttivita<=@AnnoDiInizioAttivita", connessione);
                comando.Parameters.AddWithValue("@AnnoDiInizioAttivita", annoDiInizioAttivita);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    var nome = (string)reader["Nome"];
                    var cognome = (string)reader["Cognome"];
                    var codiceFiscale = (string)reader["CodiceFiscale"];
                    var area = (string)reader["AreaGeografica"];
                    annoDiInizioAttivita = (int)reader["AnnoDiInizioAttivita"];
                    Agente a = new Agente(nome, cognome, codiceFiscale, area, annoDiInizioAttivita);
                    Console.WriteLine(a.StampaDati());
                }
            }
        }

        public void GetAllAgents()
        {
            using (SqlConnection connessione = new SqlConnection(connectionString))
            {
                connessione.Open();
                SqlCommand comando = new SqlCommand("select * from Agente", connessione);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    var nome = (string)reader["Nome"];
                    var cognome = (string)reader["Cognome"];
                    var codiceFiscale = (string)reader["CodiceFiscale"];
                    var area = (string)reader["AreaGeografica"];
                    var annoDiInizioAttivita = (int)reader["AnnoDiInizioAttivita"];
                    Agente a = new Agente(nome, cognome, codiceFiscale, area, annoDiInizioAttivita);
                    Console.WriteLine(a.StampaDati());                    
                }
                connessione.Close();
            }
        }

        public void InserisciAgente(Agente agente)
        {
            using (SqlConnection connessione = new SqlConnection(connectionString))
            {
                connessione.Open();
                SqlCommand comando = new SqlCommand("insert into Agente values(@Nome,@Cognome,@CodiceFiscale,@AreaGeografica,@AnnoDiInizioAttivita)", connessione);
                comando.Parameters.AddWithValue("@Nome", agente.Nome);
                comando.Parameters.AddWithValue("@Cognome", agente.Cognome);
                comando.Parameters.AddWithValue("@CodiceFiscale", agente.CodiceFiscale);
                comando.Parameters.AddWithValue("@AreaGeografica", agente.AreaGeografica);
                comando.Parameters.AddWithValue("@AnnoDiInizioAttivita", agente.AnnoDiInizioAttivita);
                int righeInserite = comando.ExecuteNonQuery();
                if (righeInserite > 0)
                {
                    Console.WriteLine("Agente inserito correttamente");
                }
                else
                {
                    Console.WriteLine("Error!!! Non è stato possibile inserire il nuovo agente");
                }
                connessione.Close();
            }
        }

        public bool VerificaInserimentoAgenteConCF(string cf)
        {
            using (SqlConnection connessione = new SqlConnection(connectionString))
            {
                connessione.Open();
                SqlCommand comando = new SqlCommand("select * from Agente where CodiceFiscale=@CodiceFiscale", connessione);
                comando.Parameters.AddWithValue("@CodiceFiscale", cf);
                SqlDataReader reader = comando.ExecuteReader();

                bool esiste = false;
                int i = 0;
                while (reader.Read())
                {
                    i++;
                }
                if (i != 0)
                {
                    esiste = true;
                    Console.WriteLine("Agente già presente");
                }
                connessione.Close();
                return esiste;
            }

        }

        public bool VerificaPresenzaAgenteArea(string area)
        {
            using (SqlConnection connessione = new SqlConnection(connectionString))
            {
                connessione.Open();
                SqlCommand comando = new SqlCommand("select * from Agente where AreaGeografica=@AreaGeografica", connessione);
                comando.Parameters.AddWithValue("@AreaGeografica", area);
                SqlDataReader reader = comando.ExecuteReader();

                bool esiste = false;
                int i = 0;
                while (reader.Read())
                {
                    i++;
                }
                if (i == 0)
                {
                    esiste = true;
                    Console.WriteLine("Nessun Agente presente in quest'area!\nInserisci un'altra area:");
                }
                connessione.Close();
                return esiste;
            }
        }
    }
}
