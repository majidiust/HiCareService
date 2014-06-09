using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;
using System.IO;
using System.Web.Hosting;
using System.Globalization;

namespace HiCareService
{
    public class BLL
    {
        DatabaseDataContext m_model = new DatabaseDataContext();
        public ValidatedUser LoginUser(string username, string password)
        {
            try
            {
                bool isValidated = System.Web.Security.Membership.ValidateUser(username, password);
                return new ValidatedUser { IsValidate = isValidated, Username = username };
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return new ValidatedUser { IsValidate = false, Username = username };
            }
        }

        public List<City> GetCities()
        {
            try
            {
                var result = new List<City>();
                foreach (var city in m_model.Cities)
                {
                    result.Add(new City
                    {
                        ID = city.ID,
                        Name = city.Name,
                        Desc = city.Desc
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public List<Country> GetCountry()
        {
            try
            {
                var result = new List<Country>();
                foreach (var city in m_model.Cities)
                {
                    result.Add(new Country
                    {
                        ID = city.ID,
                        Name = city.Name,
                        Desc = city.Desc
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public List<Degree> GetDegree()
        {
            try
            {
                var result = new List<Degree>();
                foreach (var city in m_model.Cities)
                {
                    result.Add(new Degree
                    {
                        ID = city.ID,
                        Name = city.Name,
                        Desc = city.Desc
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public Profile GetUserProfile(string username)
        {
            try
            {
                if (m_model.aspnet_Users.Count(P => P.UserName.Equals(username)) > 0)
                {
                    var user = m_model.aspnet_Users.Single(P => P.UserName.Equals(username));
                    if (m_model.Profiles.Count(P => P.UserID.Equals(user.UserId)) > 0)
                    {
                        Profile tmpResult = m_model.Profiles.Single(P => P.UserID.Equals(user.UserId));
                        Profile result = new Profile
                        {
                            Age = tmpResult.Age,
                            FirstName = tmpResult.FirstName,
                            LastName = tmpResult.LastName,
                            DegreeID = tmpResult.DegreeID,
                            CityID = tmpResult.CityID,
                            IsValidate = tmpResult.IsValidate,
                            CountryID = tmpResult.CountryID,
                            NationalityCode = tmpResult.NationalityCode,
                            Gender = tmpResult.Gender,
                            ProfilePicture = tmpResult.ProfilePicture
                        };
                        return result;
                    }
                    else return null;
                }
                else return null;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public List<PatientSummary> GetListOfPatient(string uname)
        {
            try
            {
                var user = m_model.aspnet_Users.Single(P => P.UserName.Equals(uname));
                var patients = m_model.DoctorPatientStatus.Where(P => P.DoctorId == user.UserId);
                List<PatientSummary> result = new List<PatientSummary>();
                foreach (var patient in patients)
                {
                    Profile p = m_model.Profiles.Single(P => P.UserID.Equals(patient.PatientId));
                    result.Add(new PatientSummary
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        NationalCode = p.NationalityCode,
                        ValidationStatus = p.IsValidate == null ? false : (bool)p.IsValidate
                    });
                }

                return result;

            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public List<CheckupSummary> GetListOfPatientCheckups(string uname)
        {
            try
            {
                var user = m_model.aspnet_Users.Single(P => P.UserName.Equals(uname));
                var checkUps = m_model.Checkups.Where(P => P.UserID == user.UserId);
                List<CheckupSummary> result = new List<CheckupSummary>();
                foreach (var checkup in checkUps)
                {
                    String Date = String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                                checkup.Date.Value.Year,
                                                checkup.Date.Value.Month,
                                                checkup.Date.Value.Day,
                                                checkup.Date.Value.Hour,
                                                checkup.Date.Value.Minute,
                                                checkup.Date.Value.Second
                                                );
                    Profile p = m_model.Profiles.Single(P => P.UserID.Equals(checkup.UserID));
                    result.Add(new CheckupSummary
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        NationalCode = p.NationalityCode,
                        Id = checkup.Id,
                        Date = Date,
                        Prescribe = checkup.Prescriptions.Count > 0 ? true : false,
                        Recommandation = checkup.Recommandations.Count > 0 ? true : false
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }


        public List<CheckupSummary> GetMyCheckups(string uname)
        {
            try
            {
                var user = m_model.aspnet_Users.Single(P => P.UserName.Equals(uname));
                var checkUps = m_model.Checkups.Where(P => P.DoctorID == user.UserId);
                List<CheckupSummary> result = new List<CheckupSummary>();
                foreach (var checkup in checkUps)
                {
                    String Date = String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                                checkup.Date.Value.Year,
                                                checkup.Date.Value.Month,
                                                checkup.Date.Value.Day,
                                                checkup.Date.Value.Hour,
                                                checkup.Date.Value.Minute,
                                                checkup.Date.Value.Second
                                                );
                    Profile p = m_model.Profiles.Single(P => P.UserID.Equals(checkup.UserID));
                    result.Add(new CheckupSummary
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        NationalCode = p.NationalityCode,
                        Id = checkup.Id,
                        Date = Date,
                        Prescribe = checkup.Prescriptions.Count > 0 ? true : false,
                        Recommandation = checkup.Recommandations.Count > 0 ? true : false
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }


        public bool IsDateInRange(DateTime begin, DateTime end, DateTime date)
        {
            if (date == null)
                return false;
            PersianCalendar pc = new PersianCalendar();
            DateTime ndate = new DateTime(pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date));
            if (ndate.Subtract(begin).TotalMilliseconds > 0 && end.Subtract(ndate).TotalMilliseconds > 0)
                return true;
            else return false;
        }

        public List<CheckupSummary> QueryRecords(string uname, string beginDate, string endDate)
        {
            try
            {
                string[] begin = beginDate.Split(new char[] { '_' });
                string[] end = endDate.Split(new char[] { '_' });
                DateTime bdate = new DateTime(
                    int.Parse(begin[0]),
                    int.Parse(begin[1]),
                    int.Parse(begin[2]),
                    int.Parse(begin[3]),
                    int.Parse(begin[4]),
                    int.Parse(begin[5]));
                DateTime edate = new DateTime(
                    int.Parse(end[0]),
                    int.Parse(end[1]),
                    int.Parse(end[2]),
                    int.Parse(end[3]),
                    int.Parse(end[4]),
                    int.Parse(end[5]));
                var user = m_model.aspnet_Users.Single(P => P.UserName.Equals(uname));
                PersianCalendar pc = new PersianCalendar();
                var checkUps = m_model.Checkups.Where(P => P.UserID == user.UserId);
                List<CheckupSummary> result = new List<CheckupSummary>();
                foreach (var checkup in checkUps)
                {
                    if (IsDateInRange(bdate, edate, (DateTime)checkup.Date) == true)
                    {
                        String Date = String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                                    checkup.Date.Value.Year,
                                                    checkup.Date.Value.Month,
                                                    checkup.Date.Value.Day,
                                                    checkup.Date.Value.Hour,
                                                    checkup.Date.Value.Minute,
                                                    checkup.Date.Value.Second
                                                    );
                        Profile p = m_model.Profiles.Single(P => P.UserID.Equals(checkup.UserID));
                        result.Add(new CheckupSummary
                        {
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            NationalCode = p.NationalityCode,
                            Id = checkup.Id,
                            Date = Date,
                            Prescribe = checkup.Prescriptions.Count > 0 ? true : false,
                            Recommandation = checkup.Recommandations.Count > 0 ? true : false
                        });
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public CheckupDescription GetCheckupDescription(string checkupId)
        {
            try
            {
                var checkup = m_model.Checkups.Single(P => P.Id == int.Parse(checkupId));
                var user = m_model.aspnet_Users.Single(P => P.UserId.Equals(checkup.UserID));
                var vitalCategoriesList = from p in m_model.VitalSigns
                                          group p.TypeID by p.VitalType.VitalCategory.Id into g
                                          select new { Category = g.Key, Vitals = g.ToList() };
                String vitalCategories = "";
                foreach (var category in vitalCategoriesList)
                    vitalCategories += category.Category + ",";
                String Date = String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                                  checkup.Date.Value.Year,
                                                  checkup.Date.Value.Month,
                                                  checkup.Date.Value.Day,
                                                  checkup.Date.Value.Hour,
                                                  checkup.Date.Value.Minute,
                                                  checkup.Date.Value.Second
                                                  );
                var profile = m_model.Profiles.Single(P => P.UserID.Equals(checkup.UserID));
                return new CheckupDescription
                {
                    Date = Date,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    NationaliId = profile.NationalityCode,
                    Prescribe = checkup.Prescriptions.Count > 0 ? true : false,
                    Recommandation = checkup.Prescriptions.Count > 0 ? true : false,
                    VitalCategories = vitalCategories,
                    Id = checkup.Id
                };
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public List<VitalSignDescription> GetVitalSign(string checkupId, string categoryId)
        {
            try
            {
                var checkup = m_model.Checkups.Single(P => P.Id == int.Parse(checkupId));
                var signs = (from p in m_model.VitalSigns
                             where p.CheckUpID == checkup.Id && p.VitalType.CategoryId == int.Parse(categoryId)
                             select new VitalSignDescription
                             {
                                 CategoryId = (int)p.VitalType.CategoryId,
                                 CategoryName = p.VitalType.VitalCategory.CategoryName,
                                 Description = p.Desc,
                                 TypeId = (int)p.TypeID,
                                 TypeName = p.VitalType.Name,
                                 Value = p.Value
                             }).ToList<VitalSignDescription>();
                String Date = String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                                  checkup.Date.Value.Year,
                                                  checkup.Date.Value.Month,
                                                  checkup.Date.Value.Day,
                                                  checkup.Date.Value.Hour,
                                                  checkup.Date.Value.Minute,
                                                  checkup.Date.Value.Second
                                                  );
                return signs;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public List<VitalType> GetVitalTypes()
        {
            try
            {
                var vitals = m_model.VitalTypes;
                List<VitalType> result = new List<VitalType>();
                foreach (var vital in vitals)
                {
                    result.Add(new VitalType
                    {
                        CategoryId = vital.CategoryId,
                        Desc = vital.Desc,
                        Id = vital.Id,
                        Name = vital.Name
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        string DateToMiladiString(DateTime date)
        {
            if (date == null)
                return "";
            PersianCalendar calendar = new PersianCalendar();
            return String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                calendar.GetYear(date),
                calendar.GetMonth(date),
                calendar.GetDayOfMonth(date),
                date.Hour,
                date.Minute,
                date.Second);
        }

        public CheckupSummary CreateNewCheckup(String username, String patientId, String deviceSerial)
        {
            try
            {
                var user = m_model.Profiles.Single(P => P.NationalityCode.Equals(username));
                var patient = m_model.Profiles.Single(P => P.NationalityCode.Equals(patientId));
                Checkup checkUp = new Checkup
                {
                    Date = DateTime.Now,
                    DeviceSerial = deviceSerial,
                    DoctorID = user.UserID,
                    Status = 1,
                    UserID = patient.UserID
                };
                m_model.Checkups.InsertOnSubmit(checkUp);

                m_model.SubmitChanges();
                return new CheckupSummary
                {
                    NationalCode = patient.NationalityCode,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Date = DateToMiladiString((DateTime)checkUp.Date),
                    Id = checkUp.Id,
                    Prescribe = false,
                    Recommandation = false
                };

            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public VitalSignDescription AddVitalSign(int checkupId, int vitalType, String Value, String Desc, bool isEdit)
        {
            try
            {
                var checkup = m_model.Checkups.Single(P => P.Id == checkupId);
                var user = m_model.Profiles.Single(P => P.UserID.Equals(checkup.DoctorID));
                var patient = m_model.Profiles.Single(P => P.UserID.Equals(checkup.UserID));
                var count = m_model.VitalSigns.Count(P => P.CheckUpID == checkupId && P.TypeID == vitalType);
                VitalSign vitalSign = new VitalSign();
                if (count > 0)
                {
                    vitalSign = m_model.VitalSigns.Single(P => P.CheckUpID == checkupId && P.TypeID == vitalType);
                    if (isEdit == true)
                    {
                        vitalSign.Value = Value;
                        vitalSign.Desc = Desc;
                        //  vitalSign.Date = DateTime.Now;
                    }
                }
                else
                {
                    vitalSign = new VitalSign();
                    vitalSign.Value = Value;
                    vitalSign.Desc = Desc;
                    //vitalSign.Date = DateTime.Now;
                    vitalSign.TypeID = vitalType;
                    vitalSign.CheckUpID = checkupId;
                    m_model.VitalSigns.InsertOnSubmit(vitalSign);
                }

                m_model.SubmitChanges();

                return new VitalSignDescription
                {
                    CategoryId = vitalSign.VitalType.CategoryId == null ? -1 : (int)vitalSign.VitalType.CategoryId,
                    CategoryName = vitalSign.VitalType.VitalCategory.CategoryName,
                    Description = vitalSign.Desc,
                    TypeId = vitalSign.TypeID == null ? -1 : (int)vitalSign.TypeID,
                    TypeName = vitalSign.VitalType.Name
                };
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public String CreateRelataedDirectories(String checkupId, String vitalCategory, String vitalType)
        {
            if (!Directory.Exists(HostingEnvironment.MapPath("Checkups")))
            {
                Directory.CreateDirectory(HostingEnvironment.MapPath("Checkups"));
            }
            if (!Directory.Exists(HostingEnvironment.MapPath("Checkups/" + checkupId)))
            {
                Directory.CreateDirectory(HostingEnvironment.MapPath("Checkups/" + checkupId));
            }
            if (!Directory.Exists(HostingEnvironment.MapPath("Checkups/" + checkupId + "/" + vitalCategory)))
            {
                Directory.CreateDirectory(HostingEnvironment.MapPath("Checkups/" + checkupId + "/" + vitalCategory));
            }

            return HostingEnvironment.MapPath("Checkups/" + checkupId + "/" + vitalCategory + "/" + vitalType);
        }

        public RegisterState AddPatient(String doctorId, String nationalcode, String firstName, String lastName, String age, String gender)
        {
            try
            {
                DoctorPatientStatus doctorPatient = new DoctorPatientStatus();
                var isExistDoctor = m_model.Profiles.Count(P => P.NationalityCode.Equals(doctorId));
                if (isExistDoctor == 0)
                    return new RegisterState
                    {
                        CreatorId = doctorId,
                        Message = "Doctor does not exist",
                        MessageId = -2,
                        Result = false,
                        UserName = nationalcode
                    };
                else
                {
                    var doctor = m_model.Profiles.Single(P => P.NationalityCode.Equals(doctorId));
                    var user = System.Web.Security.Membership.GetUser(nationalcode);
                    if (user == null)
                    {
                        var newUser = System.Web.Security.Membership.CreateUser(nationalcode, nationalcode);
                        var userObject = m_model.aspnet_Users.Single(P => P.UserName.Equals(nationalcode));
                        var newProfile = new Profile();
                        newProfile.UserID = userObject.UserId;
                        newProfile.FirstName = firstName;
                        newProfile.LastName = lastName;
                        newProfile.NationalityCode = nationalcode;
                        newProfile.IsValidate = true;
                        newProfile.Gender = bool.Parse(gender);
                        //TODO : Correct City, Country, Degree
                        newProfile.CityID = 4;
                        newProfile.DegreeID = 3;
                        newProfile.CountryID = 1;
                        newProfile.Age = int.Parse(age);
                        m_model.Profiles.InsertOnSubmit(newProfile);
                        DoctorPatientStatus relation = new DoctorPatientStatus();
                        relation.DoctorId = doctor.UserID;
                        relation.PatientId = userObject.UserId;
                        relation.State = 1;
                        m_model.DoctorPatientStatus.InsertOnSubmit(relation);
                        m_model.SubmitChanges();
                        return new RegisterState
                        {
                            Message = "Added Successfully.",
                            Result = true,
                            UserName = nationalcode,
                            MessageId = 0,
                            CreatorId = doctorId
                        };
                    }
                    else
                    {
                        return new RegisterState
                        {
                            Message = "This User Exist",
                            Result = false,
                            UserName = nationalcode,
                            MessageId = -1,
                            CreatorId = doctorId
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return new RegisterState
                    {
                        Message = "Error in create user",
                        Result = false,
                        UserName = nationalcode,
                        MessageId = -3,
                        CreatorId = doctorId
                    };
            }
        }

        public List<PrescribeResult> GetListofPrescribeForVisit(String checkupId)
        {
            try
            {
                bool isExistCheckup = m_model.Checkups.Count(p => p.Id == int.Parse(checkupId)) > 0;
                if (isExistCheckup)
                {
                    var prescribes = (from p in m_model.Prescriptions where p.CheckupID == int.Parse(checkupId) select new PrescribeResult {
                        Date = DateToMiladiString((DateTime)p.Date),
                        ResultState = true,
                        ResultMessage = "Prescribe Fetched Successfully",
                        ResultId = 1,
                        Desc = p.Desc,
                        CheckupId = checkupId,
                        CheckkupDate = DateToMiladiString((DateTime)p.Checkup.Date),
                        DoctorId = p.aspnet_User.UserName,
                        DoctorFisrtName = p.aspnet_User.Profiles[0].FirstName,
                        DoctorLastName = p.aspnet_User.Profiles[0].LastName
                    }).ToList<PrescribeResult>();
                    return prescribes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return null;
            }
        }

        public PrescribeResult AddPrescribe(String doctorId, String checkupId, String content)
        {
            try
            {
                bool isExistUser = m_model.Profiles.Count(p => p.NationalityCode.Equals(doctorId)) > 0;
                if (isExistUser)
                {
                    bool isExistCheckup = m_model.Checkups.Count(p => p.Id == int.Parse(checkupId)) > 0;
                    if (isExistCheckup)
                    {
                        var doctor = m_model.Profiles.Single(p => p.NationalityCode.Equals(doctorId));
                        var checkup = m_model.Checkups.Single(p => p.Id == int.Parse(checkupId));
                        Prescription recom = new Prescription();
                        recom.Desc = content;
                        recom.DoctorID = doctor.UserID;
                        recom.CheckupID = int.Parse(checkupId);
                        recom.Date = DateTime.UtcNow;
                        m_model.Prescriptions.InsertOnSubmit(recom);
                        m_model.SubmitChanges();
                        return new PrescribeResult
                        {
                            Date = DateToMiladiString((DateTime)recom.Date),
                            ResultState = true,
                            ResultMessage = "Prescribe Added Successfully",
                            ResultId = 1,
                            Desc = recom.Desc,
                            CheckupId = checkupId,
                            CheckkupDate = DateToMiladiString((DateTime)checkup.Date),
                            DoctorId = doctorId,
                            DoctorFisrtName = doctor.FirstName,
                            DoctorLastName = doctor.LastName
                        };
                    }
                    else
                    {
                        return new PrescribeResult
                        {
                            ResultState = false,
                            ResultId = -4,
                            ResultMessage = "Checkup Does Not Exist"
                        };
                    }
                }
                else
                {
                    return new PrescribeResult
                    {
                        ResultState = false,
                        ResultId = -2,
                        ResultMessage = "Doctor Does Not Exist"
                    };
                }
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
                return new PrescribeResult
                {
                    ResultState = false,
                    ResultId = -3,
                    ResultMessage = ex.Message
                };
            }
        }

        public void AddStreamVitalSing(int checkupId, int vitalType, Stream fileStream)
        {
            try
            {
                var vitalCategory = m_model.VitalTypes.Single(P => P.Id == vitalType);
                String fileName = CreateRelataedDirectories(checkupId.ToString(), vitalCategory.Id.ToString(), vitalType.ToString());
                FileStream fileToupload = new FileStream(fileName, FileMode.Create);

                byte[] bytearray = new byte[10000];
                int bytesRead, totalBytesRead = 0;
                do
                {
                    bytesRead = fileStream.Read(bytearray, 0, bytearray.Length);
                    totalBytesRead += bytesRead;
                } while (bytesRead > 0);

                fileToupload.Write(bytearray, 0, bytearray.Length);
                fileToupload.Close();
                fileToupload.Dispose();
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.StatusDescription = ex.Message;
            }
        }
    }
}