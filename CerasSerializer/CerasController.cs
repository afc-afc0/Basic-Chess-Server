using Ceras;

namespace Basic_Chess_Server.Ceras
{
    class CerasController
    {

        #region Singleton
        private static CerasController instance;
        public static CerasController Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        private CerasSerializer ceras;

        public CerasController()
        {
            ceras = new CerasSerializer();
            instance = this;
        }

        public CerasSerializer GetCeras()
        {
            return ceras;
        }

    }

}
