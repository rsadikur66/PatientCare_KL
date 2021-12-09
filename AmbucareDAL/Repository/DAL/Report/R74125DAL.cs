using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AmbucareDAL.Models;

namespace AmbucareDAL.Repository.DAL.Report
{
    public class R74125DAL : CommonDAL
    {
        public DataTable GetPatInformation(string T_REQUEST_ID)
        {
            return Query(
                $"SELECT T74041.T_REQUEST_ID, T74046.T_FIRST_LANG2_NAME ||' ' ||T74046.T_FATHER_LANG2_NAME ||' ' ||T74046.T_GFATHER_LANG2_NAME ||' ' ||T74046.T_MOTHER_LANG2_NAME PAT_NAME, T74041.T_AGE, T_DISCH_RSN_REMARKS, T74050.T_SHORT_GNDR_NAME, T74046.T_MOBILE_NO, T74046.T_ADDRESS1, T74046.T_ADDRESS2, T74046.T_PASSPORT_NO FROM T74041 LEFT JOIN T74046 ON T74041.T_PAT_ID=T74046.T_PAT_ID LEFT JOIN T74050 ON T74050.T_SEX_CODE =T74046.T_SEX_CODE WHERE T74041.T_REQUEST_ID='{T_REQUEST_ID}'");
        }

        public DataTable GetAdministeredMedicine(string T_REQUEST_ID)
        {
            return Query($"SELECT DISTINCT T74044.T_STORE_ID,T74041.T_AMBU_REG_ID,T74073.T_LANG2_NAME,fn_Stock_Qty_Instant(t74027.T_ITEM_ID,t74044.T_STORE_ID) STOCK ,t74027.T_ITEM_ID ,(SELECT DISTINCT T74037.T_ITEM_ID  FROM t74037 JOIN T74036 ON T74037.T_ISSUE_ID = T74036.T_ISSUE_ID  WHERE t74036.t_request_id=t74041.t_request_id  AND T74037.T_ITEM_ID =t74027.T_ITEM_ID ) ISSED_ITEM,(SELECT SUM(ROUND(T74037.T_QUANTITY*T74003.T_QTY)) ADMINISTED_QTY  FROM t74037  JOIN T74036  ON T74037.T_ISSUE_ID = T74036.T_ISSUE_ID  JOIN T74003  ON T74037.T_UOM_ID = T74003.T_ITEM_UM_ID  WHERE t74036.t_request_id=t74041.t_request_id  AND T74037.T_ITEM_ID=t74027.T_ITEM_ID  ) ADMINISTERED_QUTY,  fn_Admin_Qty(t74027.T_ITEM_ID,'{T_REQUEST_ID}') T_DOSE_DURATION_CODE FROM T74041 JOIN T74044 ON T74041.T_AMBU_REG_ID = T74044.T_AMBU_REG_ID JOIN t74027 ON T74044.T_STORE_ID = t74027.T_STORE_ID JOIN T74073 ON t74027.T_ITEM_ID = T74073.T_ID LEFT JOIN T74039 ON T74041.T_REQUEST_ID = T74039.T_REQUEST_ID LEFT JOIN T74040 ON T74039.T_PRESCRIPTION_ID = T74040.T_PRESCRIPTION_ID WHERE T_COST_TYPE_ID ='23' AND t74041.t_request_id='{T_REQUEST_ID}' GROUP BY T74044.T_STORE_ID,T74041.T_AMBU_REG_ID,t74027.T_ITEM_ID,T74073.T_LANG2_NAME,T_DOSE_DURATION_CODE,T74040.T_PRESCRIPTION_ID,t74041.t_request_id ORDER BY T74073.T_LANG2_NAME ASC");
        }

        public DataTable GetVitalSignData(string T_REQUEST_ID)
        {
            string SPO2 = null;
            string Resp = null;
            string Urine = null;
            string Time = null;
            string Pulse = null;
            string BP = null;
            string Temp = null;




            DataTable dt=Query($"select T_LANG2_TEXT labelName from t74000 where T_FORM_CODE='R74125' and T_LABEL_NAME in ('lblTime','lblPulse','lblBP','lblSpo2','lblResp','lblUrine','lblTemp')");
            if (dt.Rows.Count>0)
            {
                SPO2 = dt.Rows[0]["LABELNAME"].ToString();
                Resp = dt.Rows[1]["LABELNAME"].ToString();
                Urine = dt.Rows[2]["LABELNAME"].ToString();
                Time = dt.Rows[3]["LABELNAME"].ToString();
                Pulse = dt.Rows[4]["LABELNAME"].ToString();
                BP = dt.Rows[5]["LABELNAME"].ToString();
                Temp = dt.Rows[6]["LABELNAME"].ToString();

            }



            return Query($"SELECT RWHDR, R1, R2, R3, R4 FROM (SELECT * FROM (SELECT ROWNUM ||'RW' RW, SUBSTR(T_TIME,11,5) ||' ' T_TIME, T_PULS ||' /dk' T_PULS, T_BP_SYS ||'/' ||T_BP_DIA ||' mmHg' T_BP_SYS, T_OS ||' %' T_OS, T_RESP ||'' T_RESP, T_URINE_TEST ||' ' T_URINE_TEST, T_TEMP ||' °C' T_TEMP FROM (SELECT * FROM t74043 WHERE T_REQUEST_ID = '{T_REQUEST_ID}' ORDER BY T_PCHECKUP_ID DESC ) WHERE ROWNUM < 5 ) pivot( MAX(T_BP_SYS) T_BP_SYS, MAX(T_TEMP) T_TEMP, MAX(T_PULS) T_PULS, MAX(T_OS) T_OS ,MAX(T_URINE_TEST) T_URINE_TEST,MAX(T_RESP) T_RESP,MAX(T_TIME) T_TIME FOR RW IN ('1RW' R1, '2RW' R2, '3RW' R3, '4RW' R4,'5RW' R5 )) unpivot((R1, R2, R3, R4,R5) FOR RWHDR IN ((R1_T_TIME, R2_T_TIME, R3_T_TIME, R4_T_TIME,R5_T_TIME) AS '{Time}',(R1_T_PULS, R2_T_PULS, R3_T_PULS, R4_T_PULS,R5_T_PULS) AS '{Pulse}',(R1_T_BP_SYS, R2_T_BP_SYS, R3_T_BP_SYS, R4_T_BP_SYS,R5_T_BP_SYS) AS '{BP}',(R1_T_OS, R2_T_OS, R3_T_OS, R4_T_OS,R5_T_OS) AS '{SPO2}',(R1_T_RESP, R2_T_RESP, R3_T_RESP, R4_T_RESP,R5_T_RESP) AS '{Resp}',(R1_T_URINE_TEST, R2_T_URINE_TEST, R3_T_URINE_TEST, R4_T_URINE_TEST,R5_T_URINE_TEST) AS '{Urine}',(R1_T_TEMP, R2_T_TEMP, R3_T_TEMP, R4_T_TEMP,R5_T_TEMP) AS '{Temp}')) )");
        }
        public DataTable GetServiceData(string T_REQUEST_ID)
        {
            return Query($"SELECT T73.T_COST_TYPE_DTL_ID,t73.T_LANG2_NAME,fn_DIS_RPT_SRVC(1,T73.T_COST_TYPE_DTL_ID,{T_REQUEST_ID})  as T1,fn_DIS_RPT_SRVC(2,T73.T_COST_TYPE_DTL_ID,{T_REQUEST_ID})  as T2   ,fn_DIS_RPT_SRVC(3,T73.T_COST_TYPE_DTL_ID,{T_REQUEST_ID})  as T3   ,fn_DIS_RPT_SRVC(4,T73.T_COST_TYPE_DTL_ID,{T_REQUEST_ID})  as T4      from t74044 t44    join  t74027 t27 on t27.t_store_id=t44.t_store_id   join t74073 t73 on t73.t_id=t27.t_item_id join t74041 t41 on T44.T_AMBU_REG_ID=T41.T_AMBU_REG_ID WHERE T41.T_REQUEST_ID ={T_REQUEST_ID}  AND t73.T_COST_TYPE_ID ='121'");
        }

        public DataTable GetLabelsData(string lang)
        {
            return Query($"SELECT * FROM(SELECT T_LABEL_NAME, T_LANG{lang}_TEXT  FROM T74000 WHERE T_FORM_CODE = 'R74125') PIVOT(MAX(T_LANG{lang}_TEXT)FOR T_LABEL_NAME IN('lblAtnNo' lblAtnNo,'lblTCSaglik' lblTCSaglik,'lblIzmirSaglik' lblIzmirSaglik,'lblUcretTahakkukuna' lblUcretTahakkukuna,'lblIzmir112' lblIzmir112,'lblAmbulansVaka' lblAmbulansVaka,'lblVakaKimlik' lblVakaKimlik,'lblOlayYeri' lblOlayYeri,'lblPupillerDeri' lblPupillerDeri,'lblVitalBulgular' lblVitalBulgular,'lblGlasgowKoma' lblGlasgowKoma,'lblIsikRefleksi' lblIsikRefleksi,'lblHavayoluSolunum' lblHavayoluSolunum,'lblResusitasyon' lblResusitasyon,'lblTriaj' lblTriaj,'lblDolasimDestegi' lblDolasimDestegi,'lblSarfMalzemeleri' lblSarfMalzemeleri,'lblIlaclar' lblIlaclar,'lblStabilizasyon' lblStabilizasyon,'lbldigerUygulamalar' lbldigerUygulamalar,'lblSerumlar' lblSerumlar,'lblTrafikKazasi' lblTrafikKazasi,'lblKlinikOn' lblKlinikOn,'lblKlinikAmbulans' lblKlinikAmbulans,'lblAciklamalar' lblAciklamalar,'lblSonuc' lblSonuc,'lblNakledilenHastane' lblNakledilenHastane,'lblAmbulansEkibi' lblAmbulansEkibi,'lblImza' lblImza,'lblHastayiTeslimHekim' lblHastayiTeslimHekim,'lblAracGorevKagidi' lblAracGorevKagidi,'lblNushaDoner' lblNushaDoner,'lblProtokolNo' lblProtokolNo,'lblEkProtNo' lblEkProtNo,'lblIstAdi' lblIstAdi,'lblIstKodu' lblIstKodu,'lblPlakaNo' lblPlakaNo,'lblCikisKm' lblCikisKm,'lblVarisKm' lblVarisKm,'lblDonusKm' lblDonusKm,'lblCagriSaati' lblCagriSaati,'lblVakayaCikis' lblVakayaCikis,'lblOlayYerineVaris' lblOlayYerineVaris,'lblYastaneyeVaris' lblYastaneyeVaris,'lblHastanedenAyrilis' lblHastanedenAyrilis,'lblIstasyonaDonus' lblIstasyonaDonus,'lblAdiSoyadi' lblAdiSoyadi,'lblTcKimlikPasaportNo' lblTcKimlikPasaportNo,'lblVakaEvAdresi' lblVakaEvAdresi,'lblBektemeNoktasi' lblBektemeNoktasi,'lblVakaTelefonNo' lblVakaTelefonNo,'lblGozAcma' lblGozAcma,'lblMotorYanit' lblMotorYanit,'lblSozelYanit' lblSozelYanit,'lblIslem' lblIslem,'lblSaat' lblSaat,'lblTriajYapilan' lblTriajYapilan,'lblIsbirligiYapilan' lblIsbirligiYapilan,'lblHastayaAitKisisel' lblHastayaAitKisisel,'lblHekimAabt' lblHekimAabt,'lblSaglikPer' lblSaglikPer,'lblSurucuSaglikPer' lblSurucuSaglikPer))");
        }
        public DataTable GetGlasgowDataE(string T_REQUEST_ID)
        {
            return Query($"SELECT T02.T_GCS_VALUE, T02.T_LANG2_NAME,T41.T_EYE_OPEN T41_VAL FROM T74102 T02 LEFT JOIN T74041  T41 ON T02.T_GCS_ID = T41.T_EYE_OPEN AND T41.T_REQUEST_ID = {T_REQUEST_ID} WHERE T02.T_GCS_TYPE = 'E' ORDER BY T_GCS_ID DESC");
        }
        public DataTable GetGlasgowDataM(string T_REQUEST_ID)
        {
            return Query($"SELECT T02.T_GCS_VALUE, T02.T_LANG2_NAME,T41.T_BEST_MOTOR T41_VAL FROM T74102 T02 LEFT JOIN T74041  T41 ON T02.T_GCS_ID = T41.T_BEST_MOTOR AND T41.T_REQUEST_ID = {T_REQUEST_ID} WHERE T02.T_GCS_TYPE = 'M'  ORDER BY T_GCS_ID DESC");
        }
        public DataTable GetGlasgowDataV(string T_REQUEST_ID)
        {
            return Query($"SELECT T02.T_GCS_VALUE, T02.T_LANG2_NAME,T41.T_VERBAL_RESPONSE T41_VAL FROM T74102 T02 LEFT JOIN T74041  T41 ON T02.T_GCS_ID = T41.T_VERBAL_RESPONSE AND T41.T_REQUEST_ID = {T_REQUEST_ID} WHERE T02.T_GCS_TYPE = 'V'  ORDER BY T_GCS_ID DESC");
        }

        public DataTable GetTriageLevel(string T_REQUEST_ID)
        {
            return Query($"WITH DUMMY_T AS (SELECT 1 TRIAGE FROM DUAL UNION ALL SELECT 2 TRIAGE FROM DUAL UNION ALL SELECT 3 TRIAGE FROM DUAL UNION ALL SELECT 4 TRIAGE FROM DUAL UNION ALL SELECT 5 TRIAGE FROM DUAL ), REAL_T AS (SELECT L.T_TRIAGE_LEVEL FROM (SELECT K.*, ROWNUM RW FROM (SELECT T_PCHECKUP_ID, T_TRIAGE_LEVEL FROM t74043 WHERE T_REQUEST_ID ={T_REQUEST_ID} AND T_TRIAGE_LEVEL IS NOT NULL ORDER BY T_PCHECKUP_ID DESC ) K ) L WHERE L.RW=1 ) SELECT DUMMY_T.TRIAGE, REAL_T.T_TRIAGE_LEVEL FROM DUMMY_T LEFT JOIN REAL_T ON DUMMY_T.TRIAGE=REAL_T.T_TRIAGE_LEVEL ORDER BY TRIAGE");
        }



    }
}