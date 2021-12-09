app.service("T74046Service", ["$http", "vrtlDirr", function ($http, vrtlDirr) {
    //For Instance Start 
    var dataSvc = {
        SaveEmployee: SaveEmployee,
        SaveDiagnosis: SaveDiagnosis,
        SaveDiagT74041: SaveDiagT74041,
        getLabelT74050: getLabelT74050,
        deleteData: deleteData,
        MaritalData: MaritalData,
        ReligionData: ReligionData,
        BloodGroupData: BloodGroupData,
        GenderData: GenderData,
        NationalityData: NationalityData,
        DesignationData: DesignationData,
        ChiefComplaintData: ChiefComplaintData,
        ProblemTypeData: ProblemTypeData,
        getAmbulance: getAmbulance,
        GetService: GetService,
        GetDiagnosis: GetDiagnosis,
        getVet_Discount: getVet_Discount,
        getAmCuRe: getAmCuRe,
        getICD10DataInSearch: getICD10DataInSearch,
        cancelData: cancelData,
        confirmDocHos: confirmDocHos,
        reqAcceptofOper: reqAcceptofOper,
        caseRecieved: caseRecieved,
        getECGImg: getECGImg,
        getStationArrivalTime: getStationArrivalTime,
        getCancelReasonDataForType3: getCancelReasonDataForType3,
        Cancel_Suggested_Hospital: Cancel_Suggested_Hospital,
        getRequestID: getRequestID,

        //---------------------------------------
        AmbulancePrice: AmbulancePrice,
        DoctorPrice: DoctorPrice,
        ServicePrice: ServicePrice,
        DiagonosisByReq: DiagonosisByReq,
        GetDocId: GetDocId,
        GetMedicineList: GetMedicineList,
        GetMedicineListByGen: GetMedicineListByGen,
        GetStock: GetStock,
        SaveBill: SaveBill,
        getHealthScreenData: getHealthScreenData,
        getId: getId,
        chkT36: chkT36,
        chkT39: chkT39,
        chkBill: chkBill,
        prvServices: prvServices,
        healthScreenAllData: healthScreenAllData,
        GPS_Insert: GPS_Insert,
        acceptPatient: acceptPatient,
        GetConLevel: GetConLevel,
        seenPatient: seenPatient,
        getDestination: getDestination,
        GetAllUserLatlong: GetAllUserLatlong,
        setHandoverHospital: setHandoverHospital,
        getSuggestedHospital: getSuggestedHospital,
        getSuggestedHospitalOper: getSuggestedHospitalOper,
        GetPatReport: GetPatReport,
        cancelAndRerequest: cancelAndRerequest,
        getGCS: getGCS,
        getCancelReasonData: getCancelReasonData,
        save26: save26,
        getArrivedDuration: getArrivedDuration,
        cleanAmbulance: cleanAmbulance,
        cleanConfirmAmbulance: cleanConfirmAmbulance,
        getNewReqId: getNewReqId,
        getMewsData: getMewsData
};
    return dataSvc;
    function MaritalData() {
        try {
            var url = vrtlDirr +'/T74046/GetMaritalData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getArrivedDuration(r) {
        try {
            var url = vrtlDirr + '/T74138/getArrivedDuration';
            var params = { req: r };
            return $http({
                url: url,
                method: "POST",
                data:params
            }).then(function(results) {
                return results.data;
            }).catch(function(ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        } 
    }

    function ReligionData() {
        try {
            var url = vrtlDirr +'/T74046/GetReligionData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function BloodGroupData() {
        try {
            var url = vrtlDirr +'/T74046/GetBloodGroupData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function GenderData() {
        try {
            var url = vrtlDirr +'/T74046/GetGenderData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function NationalityData() {
        try {
            var url = vrtlDirr +'/T74046/GetNationality';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function DesignationData() {
        try {
            var url = vrtlDirr +'/T74046/GetDesignation';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function ChiefComplaintData() {
        try {
            var url = vrtlDirr +'/T74046/GetChiefComplaintData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function ProblemTypeData() {
        try {
            var url = vrtlDirr +'/T74046/GetProblemTypeData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getAmbulance(AmbID) {
        try {
            var url = vrtlDirr +'/T74046/GetAmbulanceData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { T_AMBU_REG_ID: AmbID }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function cleanAmbulance() {
        try {
            var url = vrtlDirr + '/T74046/CleanAmbulance';
            var params = {};
            return $http({
                url: url,
                method: 'POST',
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (e) {
            throw e;
        }
    }

    function cleanConfirmAmbulance(t74117) {
        try {
            var url = vrtlDirr + '/T74046/CleanConfirmAmbulance';
            var params = { t74117: t74117};
            return $http({
                url: url,
                method: 'POST',
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (e) {
            throw e;
        }
    }

    function GetService(AmbID) {
        try {
            var url = vrtlDirr +'/T74046/GetServiceData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { T_AMBU_REG_ID: AmbID }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function GetDiagnosis(Request) {
        try {
            var url = vrtlDirr +'/T74046/GetDiagnosis';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { T_REQUEST_ID: Request }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getVet_Discount() {
        try {
            var url = vrtlDirr +'/T74046/Vet_Discount';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
                // data: { T_REQUEST_ID: Request }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getAmCuRe(reqId) {
        try {
            var url = vrtlDirr +'/T74046/GetAmCuRe';
           // var params = { reqId: reqId};
            return $http({
                url: url,
                method: "POST",
               // data: { reqId: reqId}
                data: { T_REQUEST_ID: reqId }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getHealthScreenData(request) {
        try {
            var url = vrtlDirr +'/T74046/getHealthScreenData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { request: request }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getICD10DataInSearch(icd) {
        try {
            var url = vrtlDirr +'/T74046/GetICD10DataInSearch';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { icd: icd }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function cancelData(can_t41) {
        try {
            var url = vrtlDirr +'/T74046/CancelData';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                // data: params
                data: { can_t41: can_t41 }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //Insert and Update Function start
    function SaveEmployee(Patient,t41) {
        debugger;
        try {
            var url = vrtlDirr +'/T74046/SaveEmployee';
            return $http({
                url: url,
                method: "POST",
                //data: JSON.stringify(Patient, t41)
                data: { _T74046: Patient, t41: t41 }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function SaveDiagnosis(Diagnosis) {

        try {
            var url = vrtlDirr +'/T74046/SaveDiagnosis';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(Diagnosis)
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function SaveDiagT74041(Diagn) {

        try {
            var url = vrtlDirr +'/T74046/SaveDiagT74041';
            return $http({
                url: url,
                method: "POST",
                data: JSON.stringify(Diagn)
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //Insert and Update Function End

    //for delete start
    function deleteData(e) {
        debugger;
        try {
            var url = vrtlDirr +'/T74112/deleteData';
            return $http({
                url: url,
                method: "POST",
                data: { T_SEX_CODE: e }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //for delete end

    function healthScreenAllData(reqID) {
        try {
            var url = vrtlDirr +'/T74046/healthScreenAllData';
            return $http({
                url: url,
                method: "POST",
                data: { reqID: reqID }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    //for label start
    function getLabelT74050() {

        try {
            var url = vrtlDirr +'/T74112/GetLabelDataT74050';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //for label end
    //Billing Part-------------Imran-------------Start
    function AmbulancePrice(e) {
        try {
            var url = vrtlDirr +'/T74046/AmbulancePrice';
            return $http({
                url: url,
                method: "POST",
                data: { T_REQUEST_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function DoctorPrice(e) {
        try {
            var url = vrtlDirr +'/T74046/DoctorPrice';
            return $http({
                url: url,
                method: "POST",
                data: { T_Doc_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function ServicePrice(e) {
        try {
            var url = vrtlDirr +'/T74046/ServicePrice';
            return $http({
                url: url,
                method: "POST",
                data: { T_REQUEST_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function DiagonosisByReq(e) {
        try {
            var url = vrtlDirr +'/T74046/DiagonosisByReq';
            return $http({
                url: url,
                method: "POST",
                data: { T_REQUEST_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetDocId(e) {
        try {
            var url = vrtlDirr +'/T74046/GetDocId';
            return $http({
                url: url,
                method: "POST",
                data: { T_REQUEST_ID: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetMedicineList(e) {
        try {
            var url = vrtlDirr +'/T74046/GetMedicineList';
            return $http({
                url: url,
                method: "POST",
                data: { T_request_Id: e }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetMedicineListByGen(T_request_Id, T_GEN_CODE, T_REQUEST_STRENGTH, T_DRUG_ROUTE_CODE, T_FORM_ID) {
        try {
            var url = vrtlDirr +'/T74046/GetMedicineListByGen';
            return $http({
                url: url,
                method: "POST",
                data: { T_request_Id: T_request_Id, T_GEN_CODE: T_GEN_CODE, T_REQUEST_STRENGTH: T_REQUEST_STRENGTH, T_DRUG_ROUTE_CODE: T_DRUG_ROUTE_CODE, T_FORM_ID: T_FORM_ID }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function GetStock(T_STORE_ID, T_ITEM_ID) {
        try {
            var url = vrtlDirr +'/T74046/GetStock';
            return $http({
                url: url,
                method: "POST",
                data: { T_STORE_ID: T_STORE_ID, T_ITEM_ID: T_ITEM_ID }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function SaveBill(t36, t37, t74, t79) {
        try {
            var url = vrtlDirr +'/T74046/SaveBill';
            return $http({
                url: url,
                method: "POST",
                data: { t74036: t36, t74037List: t37, t74074: t74, t74079List: t79 }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getId() {
        try {
            var url = '/T74046/getId';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getId() {
        try {
            var url = vrtlDirr +'/T74046/getId';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function chkT36(m) {
        try {
            var url = vrtlDirr +'/T74046/chkT36';
            return $http({
                url: url,
                method: "POST",
                data: { id: m }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function chkT39(m) {
        try {
            var url = vrtlDirr +'/T74046/chkT39';
            return $http({
                url: url,
                method: "POST",
                data: { id: m }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function chkBill(m) {
        try {
            var url = vrtlDirr +'/T74046/chkBill';
            return $http({
                url: url,
                method: "POST",
                data: { rId: m }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function prvServices(m) {
        try {
            var url = vrtlDirr +'/T74046/prvServices';
            return $http({
                url: url,
                method: "POST",
                data: { rId: m }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //Billing Part-------------Imran-------------End
    function GPS_Insert(latitude, longitude) {
        try {
            var url = vrtlDirr +'/T74046/GPS_Insert';
            var params = { latitude: latitude, longitude: longitude };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function acceptPatient(requId) {
        try {
            var url = vrtlDirr +'/T74046/AcceptPatient';
            var params = { requId: requId };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetConLevel() {
        try {
            var url = vrtlDirr +'/T74046/GetConLevel';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function seenPatient(requId) {
        try {
            var url = vrtlDirr +'/T74046/SeenPatient';
            var params = { requId: requId };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }

    function getDestination(e) {
        try {
            var url = vrtlDirr +'/T74138/getDestination';
            var params = {e:e};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function GetAllUserLatlong() {
        try {
            var url = vrtlDirr +'/T74138/GetAllUserLatlong';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function setHandoverHospital(e) {
        try {
            var url = vrtlDirr +'/T74138/setHandoverHospital';
            var params = {site:e};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getSuggestedHospital(requID) {
        try {
            var url = vrtlDirr +'/T74046/getSuggestedHospital';
            var params = { requID: requID };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getSuggestedHospitalOper(requID) {
        try {
            var url = vrtlDirr +'/T74046/getSuggestedHospitalOper';
            var params = { requID: requID };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function confirmDocHos(r,s,t) {
        try {
            var url = vrtlDirr +'/T74046/confirmDocHos';
            var params = { T_REQUEST_ID: r, T_SITE_CODE: s, T_ROLE_CODE:t};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function Cancel_Suggested_Hospital(r, s) {
        try {
            var url = vrtlDirr + '/T74046/Cancel_Suggested_Hospital';
            var params = { T_REQUEST_ID: r, T_SITE_CODE: s};
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getStationArrivalTime(requID) {
        try {
            var url = vrtlDirr + '/T74046/getStationArrivalTime';
            var params = { requID: requID };
            return $http({
                url: url,
                method: "POST",
                data: params
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    //for Patient report
   
    //function GetPatReport(T_REQUEST_ID) {
    //    try {
    //        var url = '/T74046/GetPatReport';
    //        return $http({
    //            url: url,
    //            method: "POST",
    //            data: { T_REQUEST_ID: T_REQUEST_ID }
    //        }).then(function (results) {
    //            return results.data;
    //        }).catch(function (ex) {
    //            throw ex;
    //        });
    //    } catch (ex) {
    //        throw ex;
    //    }
    //}

    function GetPatReport(T_REQUEST_ID) {
        try {
            var url = vrtlDirr +'/T74046Report/GetPatReport';
            return $http({
                url: url,
                method: "POST",
                data: { T_REQUEST_ID: T_REQUEST_ID }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function cancelAndRerequest(ambID, can_t41) {
        try {
            var url = vrtlDirr +'/T74046/CancelAndRerequest';
            return $http({
                url: url,
                method: "POST",
                data: { ambID: ambID, can_t41: can_t41}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getGCS() {
        try {
            var url = vrtlDirr +'/T74046/getGCS';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getCancelReasonData() {
        try {
            var url = vrtlDirr+'/T74046/GetCancelReasonData';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function save26(t74026) {
        try {
            var url = vrtlDirr +'/Menu/SaveLatLong';
            return $http({
                url: url,
                method: "POST",
                data: { t74026: t74026}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function caseRecieved(e) {
        try {
            var url = vrtlDirr +'/T74046/caseRecieved';
            return $http({
                url: url,
                method: "POST",
                data: { requId:e}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function reqAcceptofOper(requId,hosCode) {
        try {
            var url = vrtlDirr +'/T74046/reqAcceptofOper';
            return $http({
                url: url,
                method: "POST",
                data: { requId: requId, hosCode: hosCode}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getECGImg(r) {
        try {
            var url = vrtlDirr + '/T74046/getECGImg';
            return $http({
                url: url,
                method: "POST",
                data: { T_REQUEST_ID: r }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getCancelReasonDataForType3() {
        try {
            var url = vrtlDirr + '/T74046/getCancelReasonDataForType3';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getRequestID() {
        try {
            var url = vrtlDirr + '/T74046/GetRequestID';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getNewReqId() {
        try {
            var url = vrtlDirr + '/T74046/GetNewReqId';
            return $http({
                url: url,
                method: "POST",
                data: {}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getMewsData(reId) {
        try {
            var url = vrtlDirr + '/T74046/getMewsData';
            return $http({
                url: url,
                method: "POST",
                data: { reId: reId}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
}]);