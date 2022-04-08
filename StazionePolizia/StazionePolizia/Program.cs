// See https://aka.ms/new-console-template for more information
using StazionePolizia;
//Risposte Teoria
//1: a-e-b-g
//2: b-d
//3: c
RepositoryPoliziaDB repository = new RepositoryPoliziaDB();
bool continua = true;
do
{
    Menu();
    int scelta;
    do
    {
        Console.Write("Fai la tua scelta: ");

    } while (!int.TryParse(Console.ReadLine(), out scelta) && scelta >= 0 && scelta <= 4);

    switch (scelta)
    {
        case 1:
            Console.WriteLine("Ecco la lista dei nostri agenti: ");
            repository.GetAllAgents();
            break;
        case 2:
            string areaG;
            do
            {
                Console.WriteLine("Inserisci l'area degli agenti che vuoi cercare: ");
                areaG = Console.ReadLine();
            } while (!repository.VerificaPresenzaAgenteArea(areaG) == false);
            repository.AgentiArea(areaG);
            break;
        case 3: 
            int anni;
            do
            {
                Console.WriteLine("Inserire gli anni di servizio: ");
            }while(!int.TryParse(Console.ReadLine(), out anni) && (anni >= 0));
            repository.AgentiConAnniDiServizio(anni);
            break;
        case 4:
            string cf;
            do
            {
                Console.WriteLine("Inserisci il Codice Fiscale del nuovo Agente:");
                cf = Console.ReadLine();
            } while (!repository.VerificaInserimentoAgenteConCF(cf) == false);            
            Console.WriteLine("Inserisci il nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci il cognome: ");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci l'area geografica a cui farà riferimento: ");
            string area = Console.ReadLine();
            int annoInizio;
            do
            {
                Console.WriteLine("Inserisci l'anno di inizio carriera: ");
            } while (!int.TryParse(Console.ReadLine(), out annoInizio) && (annoInizio >= 0));
            Agente newAgente = new Agente(nome, cognome, cf, area, annoInizio);            
            repository.InserisciAgente(newAgente);
            break;
        case 0:
            Console.WriteLine("Ciao e buona giornata!");
            continua = false;
            break;
    }


} while (continua);



void Menu()
{
    Console.WriteLine("\n-------------------------------------Menù---------------------------------------------");
    Console.WriteLine("1. Visualizzare tutti gli agenti di polizia");
    Console.WriteLine("2. Mostrare gli agenti assegnati ad una determinata area");
    Console.WriteLine("3. Mostrare gli agenti con anni di servizio maggiori o uguali ad una determinata cifra");
    Console.WriteLine("4. Inserire un nuovo agente");
    Console.WriteLine("0. Exit!");
    Console.WriteLine("--------------------------------------------------------------------------------------");
    Console.WriteLine();
}