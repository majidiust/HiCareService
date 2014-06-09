using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using System.IO;

namespace HiCareService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HiCareService
    {
        BLL m_bll = new BLL();
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Account/login/json/{username}/{password}")]
        public ValidatedUser LoginUserJson(string username, string password)
        {
            return m_bll.LoginUser(username, password);
        }

        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Xml,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "Account/login/xml/{username}/{password}")]
        public ValidatedUser LoginUserXML(string username, string password)
        {
            return m_bll.LoginUser(username, password);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Xml,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Account/GetUser/xml/{username}")]
        public Profile GetUserProfileXML(string username)
        {
            return m_bll.GetUserProfile(username);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Account/GetUser/json/{username}")]
        public Profile GetUserProfileJSON(string username)
        {
            return m_bll.GetUserProfile(username);
        }


        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Xml,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Utility/GetCities/xml")]
        public List<City> GetCitiesXML()
        {
            return m_bll.GetCities();
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Utility/GetCities/json")]
        public List<City> GetCitiesJSON()
        {
            return m_bll.GetCities();
        }

        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Utility/GetCountries/xml")]
        public List<Country> GetCountriesXML()
        {
            return m_bll.GetCountry();
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Utility/GetCountries/json")]
        public List<Country> GetCountriesJson()
        {
            return m_bll.GetCountry();
        }

        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Utility/GetDegrees/xml")]
        public List<Degree> GetDegreesXML()
        {
            return m_bll.GetDegree();
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Utility/GetDegrees/json")]
        public List<Degree> GetDegreesJSON()
        {
            return m_bll.GetDegree();
        }

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Patient/GetList/xml/{username}")]
        public List<PatientSummary> GetPatientListByDoctorXML(string username)
        {
            return m_bll.GetListOfPatient(username);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetList/json/{username}")]
        public List<PatientSummary> GetPatientListByDoctorJSON(string username)
        {
            return m_bll.GetListOfPatient(username);
        }

        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Xml,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "Patient/GetCheckups/xml/{username}")]
        public List<CheckupSummary> GetPatientCheckupsXML(string username)
        {
            return m_bll.GetListOfPatientCheckups(username);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetCheckups/json/{username}")]
        public List<CheckupSummary> GetPatientCheckupsJSON(string username)
        {
            return m_bll.GetListOfPatientCheckups(username);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Xml,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetMyCheckups/xml/{username}")]
        public List<CheckupSummary> GetMyCheckupsXML(string username)
        {
            return m_bll.GetMyCheckups(username);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetMyCheckups/json/{username}")]
        public List<CheckupSummary> GetMyCheckupsJSON(string username)
        {
            return m_bll.GetMyCheckups(username);
        }


        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Patient/QueryRecords/xml/{username}/{begindate}/{enddate}")]
        public List<CheckupSummary> QueryRecordsXML(string username, string beginDate, string endDate)
        {
            return m_bll.QueryRecords(username, beginDate, endDate);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/QueryRecords/json/{username}/{begindate}/{enddate}")]
        public List<CheckupSummary> QueryRecordsJSON(string username, string beginDate, string endDate)
        {
            return m_bll.QueryRecords(username, beginDate, endDate);
        }


        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Xml,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "Patient/GetCheckupDescription/xml/{checkupId}")]
        public CheckupDescription GetCheckupDescriptionXML(string checkupId)
        {
            return m_bll.GetCheckupDescription(checkupId);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetCheckupDescription/json/{checkupId}")]
        public CheckupDescription GetCheckupDescriptionJSON(string checkupId)
        {
            return m_bll.GetCheckupDescription(checkupId);
        }

        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Xml,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "DateTime/Sync/xml")]
        public TimeSyncResult TimeSyncXML()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(1970, 1, 9, 0, 0, 00);
            TimeSpan ts = now - startDate;

            return new TimeSyncResult
            {
                ms = ts.TotalMilliseconds.ToString(),
                ts = ts.TotalSeconds.ToString()
            };
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "DateTime/Sync/json")]
        public TimeSyncResult TimeSyncJSON()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 00);
            TimeSpan ts = now - startDate;

            return new TimeSyncResult
            {
                ms = ts.TotalMilliseconds.ToString(),
                ts = ts.TotalSeconds.ToString()
            };
        }


        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Xml,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/GetVitalSigns/xml/{checkupId}/{categoryId}")]
        public List<VitalSignDescription> GetVitalSignsXML(string checkupId, string categoryId)
        {
            return m_bll.GetVitalSign(checkupId, categoryId);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetVitalSigns/json/{checkupId}/{categoryId}")]
        public List<VitalSignDescription> GetVitalSignsJSON(string checkupId, string categoryId)
        {
            return m_bll.GetVitalSign(checkupId, categoryId);
        }

        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Xml,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/GetVitalTypes/xml")]
        public List<VitalType> GetVitalTypesXML()
        {
            return m_bll.GetVitalTypes();
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetVitalTypes/json")]
        public List<VitalType> GetVitalTypesJSON()
        {
            return m_bll.GetVitalTypes();
        }

        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Xml,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/CreateCheckup/xml/{username}/{patientId}/{deviceSerial}")]
        public CheckupSummary CreatecheckupXML(string username, string patientId, string deviceSerial)
        {
            return m_bll.CreateNewCheckup(username, patientId, deviceSerial);
        }

        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/CreateCheckup/json/{username}/{patientId}/{deviceSerial}")]
        public CheckupSummary CreatecheckupJSON(string username, string patientId, string deviceSerial)
        {
            return m_bll.CreateNewCheckup(username, patientId, deviceSerial);
        }


        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Xml,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/AddVitalSign/xml/{checkupId}/{vitalType}/{Value}/{Desc}")]
        public VitalSignDescription AddVitalSignsXML(string checkupId, string vitalType, string Value, String Desc)
        {
            return m_bll.AddVitalSign(int.Parse(checkupId), int.Parse(vitalType), Value, Desc, false);
        }

        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/AddVitalSign/json/{checkupId}/{vitalType}/{Value}/{Desc}")]
        public VitalSignDescription AddVitalSignsJSON(string checkupId, string vitalType, string Value, String Desc)
        {
            return m_bll.AddVitalSign(int.Parse(checkupId), int.Parse(vitalType), Value, Desc, false);
        }

        [WebInvoke(Method = "POST",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/AddStreamVitalSign/json/{checkupId}/{vitalType}")]
        public void AddStreamVitalSignJSON(string checkupId, string vitalType, Stream fileStream)
        {
            m_bll.AddStreamVitalSing(int.Parse(checkupId), int.Parse(vitalType), fileStream);
        }

        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/AddPatient/json/{doctorId}/{nationalcode}/{firstName}/{lastName}/{age}/{gender}")]
        public RegisterState AddPatientJSON(String doctorId, String nationalcode, String firstName, String lastName, String age, String gender)
        {
            return m_bll.AddPatient(doctorId, nationalcode, firstName, lastName, age, gender);
        }

        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Xml,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/AddPatient/xml/{doctorId}/{nationalcode}/{firstName}/{lastName}/{age}/{gender}")]
        public RegisterState AddPatientXML(String doctorId, String nationalcode, String firstName, String lastName, String age, String gender)
        {
            return m_bll.AddPatient(doctorId, nationalcode, firstName, lastName, age, gender);
        }

        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "Patient/GetPrescribe/json/{checkupid}")]
        public List<PrescribeResult> GetPrescibesJSON(String checkupId)
        {
            return m_bll.GetListofPrescribeForVisit(checkupId);
        }

        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Xml,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "Patient/GetPrescribe/xml/{checkupid}")]
        public List<PrescribeResult> GetPrescibesXML(String checkupId)
        {
            return m_bll.GetListofPrescribeForVisit(checkupId);
        }


        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Patient/AddPrescribe/json/{doctorId}/{checkupId}/{desc}")]
        public PrescribeResult AddPrescibesJSON(String doctorId, String checkupId, String desc)
        {
            return m_bll.AddPrescribe(doctorId, checkupId, desc);
        }

        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Patient/AddPrescribe/xml/{doctorId}/{checkupId}/{desc}")]
        public PrescribeResult AddPrescibesXML(String doctorId, String checkupId, String desc)
        {
            return m_bll.AddPrescribe(doctorId, checkupId, desc);
        }
    }
}
