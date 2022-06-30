using LojaVeiculos.Models;
using LojaVeiculos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaVeiculos.Aplicacao
{
    public class LogAplicacao
    {
        public int GravarLog(Log log)
        {
            try
            {
                using(var logs = new LogRepositorio())
                {
                    logs.Adicionar(log);
                    logs.SalvarTodos();

                    return log.LogId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Log> ListarLogs(int id, string processo)
        {
            try
            {
                using (var logs = new LogRepositorio())
                {
                    return logs.GetAll().Where(x => x.CodReferencia == id && x.Processo == processo).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}