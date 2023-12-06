using System;

namespace IndigoErp.Models;

public class ReportModel : PadraoViewModel
    {
    public int EmployeeId { get; set; }
    public string Section { get; set; }
    public string Origin { get; set; }
    public string FailType { get; set; }
    public string FailCause { get; set; }
    public DateTime? InitialDate { get; set; }
    public DateTime? FinalDate { get; set; }
    public string Description { get; set; }
    public DateTime? Begin { get; set; }
    public DateTime? End { get; set; }
    public string MaintenceTYpe { get; set; }
    public string Status { get; set; }
    }
