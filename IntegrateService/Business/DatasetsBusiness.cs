using System;
using System.Collections.Generic;
using System.Text;
using General;
using Main.Interface;
using Main.Model;
using Repository;

namespace Business
{
    public class DatasetsBusiness : IDatasetsBusiness
    {
        private readonly DatasetsRepository datasetsRepository;
        public DatasetsBusiness()
        {
            datasetsRepository = new DatasetsRepository();
        }
        public bool AddDatasets(Dataset model)
        {
            bool rs = false;
            try
            {
                if (datasetsRepository.AddDatasets(model) > 0)
                {
                    rs = true;
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rs;
        }
    }
}
