using General;
using Main;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class DatasetBusiness : IDatasetBusiness
    {
        private readonly DatasetRepository datasetRepository;

        public DatasetBusiness()
        {
            this.datasetRepository = new DatasetRepository();
        }
        public bool AddDataset(Dataset model)
        {
            bool rsBool = false;
            try
            {
                if (datasetRepository.AddDataset(model) > 0)
                {
                    rsBool = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return rsBool;
        }
    }
}
