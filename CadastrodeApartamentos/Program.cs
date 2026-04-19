// See https://aka.ms/new-console-template for more information

using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using MySqlConnector;

public class MyClass
{

    static void Main(String[] args)
    {
        MyClass myClass = new MyClass();

        Iniciar();

    }

    static void Iniciar()
    {

        bool funcionando = true;


        while (funcionando)
        {
            Console.Clear();

            Console.WriteLine("Olá Seja bem vindo ao sistema de Apartamentos!\n");
            Console.WriteLine("Escolha a Função Desejada digitando o Número correspondente");
            Console.WriteLine("1 - Cadastrar novos Moradores");
            Console.WriteLine("2 - Excluir Antigos Moradores");
            Console.WriteLine("3 - Lista de Todos Moradores");
            Console.WriteLine("4 - Sair");

            int option;
            int.TryParse(Console.ReadLine(), out option);

            Console.Clear();

            switch (option)
            {
                case 1:
                    String opMorador;

                    do
                    {
                        Console.WriteLine("Digite o nome do Morador: ");
                        String nome = (Console.ReadLine() ?? "");
                        Console.WriteLine("Digite o andar do Apartamento: ");
                        int andar = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digiter o Numero do Apartamento: ");
                        int apto = int.Parse(Console.ReadLine());

                        Console.WriteLine("Há mais algum Morador para cadastrar?");
                        Console.WriteLine("S/N");
                        opMorador = (Console.ReadLine() ?? "").ToLowerInvariant();

                        InserirMorador(nome, andar, apto);

                    }
                    while (opMorador == "s");
                    

                    break;

                case 2:
                    Console.WriteLine();

                    break;

                case 3:

                    ListarMoradores();
                    break;

                default:
                    funcionando = false;
                    break;
            }
        }
    }
    static MySqlConnection Connection()
    {

        String conexao = "Server=localhost;Database=db_predio;Uid=root;Pwd=;";    

        MySqlConnection conn = new MySqlConnection(conexao);
        conn.Open();
        return conn;

    }

    static void InserirMorador(String nome,int andar, int apto)
    {
        try
        {
            using (var conn = Connection())
            {
                String query = "INSERT INTO moradores (nome_morador, andar, apto) VALUES (@nome, @andar, @apto)";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@andar", andar);
                cmd.Parameters.AddWithValue("@apto", apto);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao Cadastrar o Usuario: {ex.Message}");
        }

    }

    static void ListarMoradores()
    {

        try
        {
            using (var conn = Connection())
            {
                String query = "SELECT * FROM moradores";

                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine();
                    Console.WriteLine($"Nome: {reader["nome_morador"]} Andar: {reader["andar"]} Apartamento: {reader["apto"]}");
                }
                Console.WriteLine("\nPressione ENTER");
                Console.ReadLine();
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Erro ao Consultar o Banco de Dados: {ex.Message}");
        }

    }

}
