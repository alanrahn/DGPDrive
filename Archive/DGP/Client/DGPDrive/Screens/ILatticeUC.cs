

namespace DGPDrive.Screens
{
    interface ILatticeUC
    {
        decimal TotalRows { get; set; }
        decimal PageSize { get; set; }
        decimal TotalPages { get; set; }
        int PageNum { get; set; }
        string GID { get; set; }

        void SetPageSize();
        void SetTotalPages();
        void SetPageLabel(int pageNum, int totalPages);
        void ClearPageInfo();
        void Search();
    }
}
