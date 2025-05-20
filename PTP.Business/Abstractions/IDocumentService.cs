using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.EntityLayer.Models;

namespace PTP.Business.Abstractions
{
    public interface IDocumentService
    {
        List<Document> GetDocumentsByProjectId(int projectId);
    }
}
