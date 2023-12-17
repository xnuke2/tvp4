using System.Collections.Generic;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Program.check();
        }
    }
    private static void check()
    {
        string key = Console.ReadLine();
        if (key == null || !key.Split(" ")[0].Trim().Equals("VAR"))
        {
            Console.WriteLine("Не опознана команда инициализации переменных");
            return;
        }
        if (!key.Split(":")[1].Trim().Equals("LOGICAL;"))
        {
            Console.WriteLine("Неправильный тип переменной");
            return;
        }
        string[] tvp = key.Split(":")[0].Split(" ");
        string a = "";
        for (int i = 1; i < tvp.Length; i++)
        {
            a = a + tvp[i];
        }
        string[] aa = a.Trim().Split(",");
        for (int i = 0; i < aa.Length; i++)
        {
            for(int j = i+1; j < aa.Length; j++)
            {
                if (aa[i].Equals(aa[j])){
                    Console.WriteLine("Повторяющиеся названия переменных");
                    return;
                }

            }
            if (!Program.IsLatinOnly(aa[i]))
            {
                Console.WriteLine("содержит не только латинские символы");
                return;
            }
            if (aa[i].Length >= 11)
            {
                Console.WriteLine("слишком длинный индификатор");
                return;
            }
        }
        key = Console.ReadLine();
        if (key == null||key.Equals(""))
        {
            Console.WriteLine("Строка пуста");
            return;
        }
        //VAR A, B, C : LOGICAL
        //BEGIN A = B.AND.C; END
        string[] keys_tmp = key.Split(" ");
        if (!keys_tmp[0].Equals("BEGIN") || !keys_tmp[keys_tmp.Length - 1].Equals("END"))
        {
            Console.WriteLine("Некорекктно написано выражение");
            return;
        }
        key = "";
        for (int i = 1; i < keys_tmp.Length - 1; i++)
        {
            key = key + keys_tmp[i];
        }

        string[] exp = key.Split(";");
        for (int i = 0; i < exp.Length - 1; i++)
        {
            if (!exp[i].Contains("="))
            {
                Console.WriteLine("Некорекктно написано выражение");
                return;
            }
            string exp_tmp = exp[i];
            exp_tmp = exp_tmp.Replace("=", "");
            if (exp_tmp.Contains(".NOT.") && !exp_tmp.Contains(".NOT.("))
            {
                Console.WriteLine("Некорекктно написано выражение");
                return;
            }

            for (int j = 0; j < aa.Length; j++)
            {
                while (exp_tmp.Contains(aa[j])) exp_tmp = exp_tmp.Replace(aa[j], "");
            }
            while (exp_tmp.Contains(".AND.")) exp_tmp = exp_tmp.Replace(".AND.", "");
            while (exp_tmp.Contains(".OR.")) exp_tmp = exp_tmp.Replace(".OR.", "");
            while (exp_tmp.Contains(".EQU.")) exp_tmp = exp_tmp.Replace(".EQU.", "");
            while (exp_tmp.Contains(".NOT.")) exp_tmp = exp_tmp.Replace(".NOT.", "");
            while (exp_tmp.Contains("0")) exp_tmp = exp_tmp.Replace("0", "");
            while (exp_tmp.Contains("1")) exp_tmp = exp_tmp.Replace("1", "");
            while (exp_tmp.Contains("(") && exp_tmp.Contains(")"))
            {
                exp_tmp = exp_tmp.Replace("(", "");
                exp_tmp = exp_tmp.Replace(")", "");
            }
            if (!exp_tmp.Equals(""))
            {
                Console.WriteLine("Некорекктно написано выражение");
                return;
            }

        }

        Console.WriteLine("Выполнено успешно");
    }
    static bool IsLatinOnly(string input)
    {
        Regex regex = new Regex("^[a-zA-Z]+$");
        return regex.IsMatch(input); 
    }
    static bool IsLatinOnly1(string input)
    {
        Regex regex = new Regex("^[\"[a-zA-Z]*+.AND.+[a-zA-Z]*\"]+$");
        return regex.IsMatch(input);
    }
    
}