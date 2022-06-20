jQuery(".form-valide").validate({
    ignore: [],
    errorClass: "invalid-feedback animated fadeInDown",
    errorElement: "div",
    errorPlacement: function (e, a) {
        jQuery(a).parents(".form-group > div").append(e)
    },
    highlight: function (e) {
        jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
    },
    success: function (e) {
        jQuery(e).closest(".form-group").removeClass("is-invalid"), jQuery(e).remove()
    },
    rules: {
        // Deviation

        "ddlSite": {
            required: !0
        },
        "ddlKategori": {
            required: !0
        },
        "txtKetKategori": {
            required: !0,
            minlength: 3
        },
        "TxtNoWO": {
            required: !0
        },
        "txtProblem": {
            required: !0            
        },
        "txtUsulanRemidial": {
            required: !0
        },
        "date-format": {
            required: !0
        },
        "ddlSiteLokasi": {
            required: !0
        },
        "ddlDeptLokasi": {
            required: !0
        },
        "txtLokasi": {
            required: !0
        },
        "txtHow": {
            required: !0
        },
        "txt30hari": {
            required: !0
        },
        "txtsama": {
            required: !0
        },
        "txtrilis": {
            required: !0
        },
        "txtlain": {
            required: !0
        },
        "txtTindakan": {
            required: !0
        },
        "ddlKualProd": {
            required: !0
        },
        "ddlRiskFin":{
            required: !0
        },
        "ddlKesehatanPer": {
            required: !0
        },
        "ddlCompliance": {
            required: !0
        },        
        "ddlRiskOrg": {
            required: !0
        },
        "ddlResLingk": {
            required: !0
        },
        "ddlRiskOpr": {
            required: !0
        },
        "ddlKeamamananP": {
            required: !0
        },
        "ddlRiskIntelek": {
            required: !0
        },
        "tblBodyWho": {
            required: !0
        }
        
    },
    messages: {
        // Daftar Conim

        "ddlSite": "Silahkan pilih Site",
        "ddlKategori": "Silahkan pilih Kategori Penyimpangan",
        "txtKetKategori": {
            required: "Silahkan isi Keterangan Kategori Penyimpagan",
            minlength: "Mohon isi Keterangan Kategori minimal 3 Huruf"
        },
        "TxtNoWO": {
            required: "Silahkan isi No Work Order Oracle"
        },
        "TxtItemCode": {
            required: "Silahkan isi Item Code Oracle terkait Penyimpangan"
        },
        "txtProblem": {
            required: "Silahkan isi Problem Penyimpagan"
        },
        "date-format": "Silahkan pilih Tanggal Penyimpangan",
        "ddlSiteLokasi": "Silahkan pilih Site Lokasi Penyimpangan",
        "ddlDeptLokasi": "Silahkan pilih Departemen Lokasi Penyimpangan",
        "txtLokasi": {
            required: "Silahkan isi Detail Lokasi Penyimpangan"
        },
        "txtHow": {
            required: "Silahkan isi Urutan Kejadian Penyimpangan"
        },
        "txt30hari": {
            required: "Silahkan isi kolom ini"
        },
        "txtsama": {
            required: "Silahkan isi kolom ini"
        },
        "txtrilis": {
            required: "Silahkan isi kolom ini"
        },
        "txtlain": {
            required: "Silahkan isi kolom ini"
        },
        "txtTindakan": {
            required: "Silahkan isi kolom Tindakan"
        },
        "txtWho": {  
            required: "Silahkan isi User Terlibat"
        },
        //"ddlKualProd" :"Silahkan pilih Kualitas Produk",
        //"ddlRiskFin": {
        //    required: "Silahkan pilih Risiko Finansial"
        //},
        //"ddlKesehatanPer": {
        //    required: "Silahkan pilih Risiko Kesehatan Personil"
        //},
        //"ddlCompliance": {
        //    required: "Silahkan pilih Compliance"
        //},
        //"ddlRiskOrg": {
        //    required: "Silahkan pilih Risiko Organisasi"
        //},
        //"ddlResLingk": {
        //    required: "Silahkan pilih Resiko Lingkungan"
        //},
        //"ddlRiskOpr":  "Silahkan pilih Risiko Operasional",
        //"ddlKeamamananP": {
        //    required: "Silahkan pilih Keamanan Personil"
        //},
        //"ddlRiskIntelek": {
        //    required: "Silahkan pilih Intelektual"
        //},
       
    }
}); 


jQuery(".form-valide2").validate({
    ignore: [],
    errorClass: "invalid-feedback animated fadeInDown",
    errorElement: "div",
    errorPlacement: function (e, a) {
        jQuery(a).parents(".form-group > div").append(e)
    },
    highlight: function (e) {
        jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
    },
    success: function (e) {
        jQuery(e).closest(".form-group").removeClass("is-invalid"), jQuery(e).remove()
    },
    rules: {
        // Deviation
        
        "txtUsulanRemidial": {
            required: !0
        },
        "ddlKualProd": {
            required: !0
        },
        "ddlRiskFin": {
            required: !0
        },
        "ddlKesehatanPer": {
            required: !0
        },
        "ddlCompliance": {
            required: !0
        },
        "ddlRiskOrg": {
            required: !0
        },
        "ddlResLingk": {
            required: !0
        },
        "ddlRiskOpr": {
            required: !0
        },
        "ddlKeamamananP": {
            required: !0
        },
        "ddlRiskIntelek": {
            required: !0
        }

    },
    messages: {
        // Daftar Conim
        "txtUsulanRemidial": {
            required: "Silahkan isi Usulan Remidial"
        },
        "ddlKualProd": "Silahkan pilih Kualitas Produk",
        "ddlRiskFin": {
            required: "Silahkan pilih Risiko Finansial"
        },
        "ddlKesehatanPer": {
            required: "Silahkan pilih Risiko Kesehatan Personil"
        },
        "ddlCompliance": {
            required: "Silahkan pilih Compliance"
        },
        "ddlRiskOrg": {
            required: "Silahkan pilih Risiko Organisasi"
        },
        "ddlResLingk": {
            required: "Silahkan pilih Resiko Lingkungan"
        },
        "ddlRiskOpr": "Silahkan pilih Risiko Operasional",
        "ddlKeamamananP": {
            required: "Silahkan pilih Keamanan Personil"
        },
        "ddlRiskIntelek": {
            required: "Silahkan pilih Intelektual"
        },

    }
}); 