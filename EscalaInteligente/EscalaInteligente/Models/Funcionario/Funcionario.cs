using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscalaInteligente.Models.Funcionario
{
    public class Funcionario
    {
        private String nome;
        private String cpfFunc;
        private int matricula;
        private DateTime horario;
        private String setor;

        public string Nome { get => nome; set => nome = value; }
        public string CpfFunc { get => cpfFunc; set => cpfFunc = value; }
        public int Matricula { get => matricula; set => matricula = value; }
        public DateTime Horario { get => horario; set => horario = value; }
        public string Setor { get => setor; set => setor = value; }
    }
}