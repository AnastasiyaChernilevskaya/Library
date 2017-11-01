using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Data.Repositories;
using System.Data;
using Library.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace Library.Services
{
    public class PeriodicalService
    {
        private PeriodicalRepository _periodicalRepository;
        public PeriodicalService()
        {
            _periodicalRepository = new PeriodicalRepository();
        }

        public List<Periodical> GetPeriodicals()
        {
            return _periodicalRepository.GetPeriodicals();
        }

        public void CreatePeriodical(Periodical periodical)
        {
            _periodicalRepository.CreatePeriodical(periodical);
        }

        public void UpdatePeriodical(Periodical periodical)
        {
            _periodicalRepository.UpdatePeriodical(periodical);
        }

        public void DestroyPeriodical(int id)
        {
            _periodicalRepository.DestroyPeriodical(id);
        }

        public Periodical GetPeriodical(int id)
        {
            return _periodicalRepository.GetPeriodical(id);
        }

        public List<Periodical> GetCheckedPeriodical()
        {
            return _periodicalRepository.GetCheckedPeriodicals();
        }
    }
}