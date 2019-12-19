using ServiceReference2;
using System;

namespace ConsoleApp4
{
    class Program
    {
        public string FtokenId = string.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string tokenId = string.Empty;

            TWRemStatus ret;
            WTpBaseClient tpBase = new WTpBaseClient();
            TWRemConfigLogin cfgLogin = new TWRemConfigLogin();

            // Funções básicas, como a de login, estão no serviço WTpBase
            cfgLogin.Empresa = 4001;
            cfgLogin.Filial = 99;
            cfgLogin.Alias = "DB4000_0099";
            cfgLogin.Depto = "ADMIN";

            cfgLogin.Usuario = "mario";
            cfgLogin.Senha = "mariogtr";

            ret = tpBase.DoLoginAlt(cfgLogin, ref tokenId);
            if (ret.Code == 0) // Sucesso no Login
            {
                Console.WriteLine("ok");

                // Prepara o serviço WTpIntegradoras para executar a operação específica desse módulo
                ServiceReference3.WTpIntegradorasClient integ;
                ServiceReference3.TWRemStatus ret2;

                integ = new ServiceReference3.WTpIntegradorasClient();
                ret2 = integ.InsDadosInteg(tokenId, "aaa");

                string msg = "Status " + ret.Code.ToString() + Environment.NewLine + ret.Msg;

                if (ret2.Code == 0)
                    Console.WriteLine("ok " + msg);
                else
                    Console.WriteLine("erro " + msg);

                // Faz o logout para liberar os recursos
                ret = tpBase.DoLogout(tokenId);

                if (ret2.Code == 0)
                    Console.WriteLine("ok " + msg);
                else
                    Console.WriteLine("erro " + msg);

            }
            else
            {
                Console.WriteLine("erro "+ret.Msg);
            }
        }
    }
}
