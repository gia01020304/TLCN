function connectToHub(nameHub, callBack) {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/" + nameHub)
        .build();
    connection.start().catch(err => console.error(err.toString()));
    connection.on("SendMessage", callBack);
}
function showRemoveImage(element) {
    var src = $(element).attr("src");
    if (src !== "/images/no-image.png") {
        $('.remove-image').show();
    } else {
        $('.remove-image').hide();
    }
}

function hasClass(ele, cls) {
    return ele.getAttribute('class').indexOf(cls) > -1;
}
function CallBackPopUpModal(resp, tableId) {

    var table = $('table[id="tbl-' + tableId + '"].tbl-crud').DataTable();
    table.ajax.reload(null, false);
    $('#modal-add-edit-tbl-' + tableId).modal("hide");
}
function getTemplateEditDel() {
    var templateAddDelButton = {
        data: null,
        title: "Action",
        className: "table-action text-center",
        defaultContent: "<a title=\"Edit\" class=\"action-icon btnEdit\"><i class=\"mdi mdi-pencil\"></i></a>"
            + "<a title=\"Delete\" class= \"action-icon btnDel\"><i class=\"mdi mdi-delete\"></i></a>"
    };
    return templateAddDelButton;
}
function createToolTip(section) {
    $(section).tooltip(
        {
            items: ".input-validation-error",
            content: function () {
                return $("[data-valmsg-for='" + $(this).attr('name') + "']").text();
            },
            classes: {
                "ui-tooltip": "custom-tooltip rounded",
                "ui-tooltip-content": "text-danger"
            },
            position: { my: "center top+15", at: "center", collision: "flipfit" }
        });
}
function showSpinner() {
    $('#spinner').show();
}
function hideSpinner() {
    $('#spinner').hide();
}
function toastDanger(text = "Server Error!.") {
    $.NotificationApp.send("Error!", text, "top-right", "rgba(0,0,0,0.2)", "error", "5000");
}

function toastSuccess(text) {
    $.NotificationApp.send("Success!", text, "top-right", "rgba(0,0,0,0.2)", "success", "4000");
}

function toastWarning(text) {
    $.NotificationApp.send("Warning!", text, "top-right", "rgba(0,0,0,0.2)", "warning", "4000");
}
(function ($) {
    $.fn.serializeObject = function () {

        var self = this,
            json = {},
            push_counters = {},
            patterns = {
                "validate": /^[a-zA-Z][a-zA-Z0-9_]*(?:\[(?:\d*|[a-zA-Z0-9_]+)\])*$/,
                "key": /[a-zA-Z0-9_]+|(?=\[\])/g,
                "push": /^$/,
                "fixed": /^\d+$/,
                "named": /^[a-zA-Z0-9_]+$/
            };


        this.build = function (base, key, value) {
            base[key] = value;
            return base;
        };

        this.push_counter = function (key) {
            if (push_counters[key] === undefined) {
                push_counters[key] = 0;
            }
            return push_counters[key]++;
        };

        $.each($(this).serializeArray(), function () {

            // skip invalid keys
            if (!patterns.validate.test(this.name)) {
                return;
            }

            var k,
                keys = this.name.match(patterns.key),
                merge = this.value,
                reverse_key = this.name;

            while ((k = keys.pop()) !== undefined) {

                // adjust reverse_key
                reverse_key = reverse_key.replace(new RegExp("\\[" + k + "\\]$"), '');

                // push
                if (k.match(patterns.push)) {
                    merge = self.build([], self.push_counter(reverse_key), merge);
                }

                // fixed
                else if (k.match(patterns.fixed)) {
                    merge = self.build([], k, merge);
                }

                // named
                else if (k.match(patterns.named)) {
                    merge = self.build({}, k, merge);
                }
            }

            json = $.extend(true, json, merge);
        });

        return json;
    };
})(jQuery);
function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}
function setImage(src, target) {

    var mes = $(src).parent().next();
    if (src.files !== undefined && src.files[0].size > 5 * 1024 * 1024) {
        $(src).val(null);
        $(target).attr('src', "");
        $(mes).text("Maximum allowed file size is 5MB");
        return false;
    }
    $(mes).text("");
    showImage(src, target);

}
function showImage(src, target) {
    var fr = new FileReader();
    fr.onload = function () {
        target.src = fr.result;
        showRemoveImage("#" + target.id);
    }
    fr.readAsDataURL(src.files[0]);

}
function loadPaging(json, table) {

    var countData = json.data.length;
    if (countData == table.page.info().start) {
        table.page('previous').draw('page');
    }
}
/**
 * 

 */
/**
 * @param {any} inforTable:{
 *  IdTable:"",
 *  IdFrm:"",
 *  FixedColumn:"",
 *  TitleModal:"",
 *  ReadAction:"",
 *  AddAction:"",
 *  EditAction:"",
 *  DelAction:""
 *  ImportAction:"",
 *  IsManage:"",
 *  CallBackProcessData:function()
 * }
 * @param {any} inforColumn:[
 * {
 *  data:"",
 *  className:"",
 *  title:"",
 * }
 * ]
 * @param {any} conditionRead:object{}
 */
function generateTable(inforTable, inforColumn, conditionRead = {}) {

    /*Add Event*/
    if (inforTable.EditAction != null) {
        $('#' + inforTable.IdTable + ' tbody').on('click', '.btnEdit', function () {
            var model = table.row($(this).parents('tr')).data();

            addEditModel(inforTable.IdTable, inforTable.IdFrm, inforTable.TitleModal, inforTable.EditAction, model, table.row($(this).parents('tr')).index());
        });
    }
    if (inforTable.DelAction != null) {
        $('#' + inforTable.IdTable + ' tbody').on('click', '.btnDel', function () {
            var cf = confirm("Are you want to delete item?");
            if (!cf) {
                return false;
            }
            var model = table.row($(this).parents('tr')).data();
            $.ajax({
                url: inforTable.DelAction,
                type: 'POST',
                dataType: 'json',
                data: model,
                success: function success(resp) {
                    if (resp.success) {
                        var table = $('table[id="' + inforTable.IdTable + '"].tbl-crud').DataTable();
                        var tb = table.ajax.reload(function (json) {
                            var table = $('table[id="' + inforTable.IdTable + '"].tbl-crud').DataTable();
                            loadPaging(json, table);
                        }, false);
                        toastSuccess(resp.msg);
                    } else {
                        toastDanger(resp.msg);
                    }
                },
                error: function error(resp) {
                    toastDanger();
                }
            });
        });
    }
    /**/


    var ajax = {
        "url": inforTable.ReadAction,
        "type": "GET",
        "datatype": "json",
        "data": conditionRead
    };
    var table = $('#' + inforTable.IdTable).DataTable({
        keys: !0,
        language: {
            paginate: {
                previous: "<i class='mdi mdi-chevron-left'>",
                next: "<i class='mdi mdi-chevron-right'>"
            }
        },
        processing: true,
        ajax: ajax,
        responsive: true,
        scrollX: true,
        fixedColumns: {
            rightColumns: (inforTable.FixedColumn === "undefined") ? 1 : inforTable.FixedColumn
        },
        order: [],
        columnDefs: [{
            "className": "dt-center align-middle text-center",
            "targets": "_all"
        }],
        drawCallback: function drawCallback(settings) {
            $(".dataTables_paginate > .pagination").addClass("pagination-rounded");
            if (typeof table !== "undefined") {
                var listButton = table.buttons();
                if (listButton !== null && listButton.length > 0) {
                    for (var i = 0; i < listButton.length; i++) {
                        var button = listButton[i];
                        if (hasClass(button.node, 'buttons-export')) {
                            if (typeof exportExcell !== "undefined" && exportExcell !== null) {
                                $('#wrap-button-export-' + inforTable.IdTable).append(button.node);
                            }
                        } else {

                            if (inforTable.IsManage && hasClass(button.node, 'btnAdd')) {
                                $("#" + inforTable.IdTable + "_length").append(button.node);
                            }
                        }
                    }
                }
                //if (isIE() == 9 && typeof inforTable.ImportAction !== "undefined" && inforTable.ImportAction !== null) {
                //    tableau('xlsxbtn-' + inforTable.IdTable, null, 'xlsx', exportExcell + '.xlsx', inforTable.IdTable);
                //}
            }

        },
        columns: inforColumn,
        buttons: [
            {
                text: 'Add new data',
                action: function action(e, dt, node, config) {
                    if (inforTable.AddAction != null) {
                        addEditModel(inforTable.IdTable, inforTable.IdFrm, inforTable.TitleModal, inforTable.AddAction, null, null);
                    }
                },
                attr: {
                    "class": 'ml-2 btn btn-primary btnAdd'
                }
            },
            //{
            //    extend: 'excelHtml5',
            //    text: '<i class="fa fa-files-o"></i> Export',
            //    className: 'btn btn-sm buttons-export',
            //    title: '',
            //    filename: exportExcell,
            //    exportOptions: {
            //        format: {
            //            header: function (data, row, column, node) {
            //                if (typeof $(column).data("exportname") !== "undefined") {
            //                    data = $(column).data("exportname");
            //                }
            //                return data.replace(/\s/g, "");
            //            },
            //            body: function body(data, row, column, node) {
            //                return "\u200C" + data;
            //            }
            //        },
            //        columns: ':not(.notForExport)'
            //    }
            //},
            //{
            //    text: '<i class="fa fa-files-o"></i> Import',
            //    className: 'btn btn-sm buttons-export ml-1',
            //    action: function action(e, dt, node, config) {
            //        if (typeof exportExcell !== "undefined" && exportExcell !== null) {
            //            handleFiles(exportExcell, inforTable.ImportAction, inforTable.IdTable);
            //        }
            //    }
            //}
        ]
    });

    //if (typeof extendActions !== "undefined" && extendActions !== null) {
    //    if (extendActions.length > 0) {
    //        extendActions.forEach(function (item) {
    //            item.action(inforTable.IdTable, table);
    //        });
    //    }
    //}
    //if (typeof inforTable.ImportAction !== "undefined" && inforTable.ImportAction !== null) {
    //    var formExport = document.createElement('form');
    //    formExport.id = "form-export-" + exportExcell;


    //    var wrapExport = document.createElement('div');
    //    wrapExport.className = '';
    //    wrapExport.id = "wrap-export-" + inforTable.IdTable;

    //    var divInputFile = document.createElement('div');
    //    divInputFile.className = "custom-file";
    //    divInputFile.innerHTML = "<input name=\"file\" onchange=\"importChange(this,'" + exportExcell + "')\" type=\"file\" class=\"custom-file-input\" id=\"import-" + exportExcell + "\" accept=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-excel\"><label id=\"label-import-" + exportExcell + "\" class=\"custom-file-label\" for=\"import-" + exportExcell + "\" > Choose file import</label >";
    //    divInputFile.style.width = 'unset';

    //    var wrapButton = document.createElement('div');
    //    wrapButton.className = " mt-2";
    //    wrapButton.id = "wrap-button-export-" + inforTable.IdTable;

    //    if (isIE() == 9) {
    //        var btnDownloadIE = document.createElement('div');
    //        btnDownloadIE.id = 'xlsxbtn-' + inforTable.IdTable;
    //        btnDownloadIE.className = "btn";
    //        wrapButton.appendChild(btnDownloadIE);
    //    }

    //    wrapExport.appendChild(divInputFile);
    //    wrapExport.appendChild(wrapButton);
    //    formExport.appendChild(wrapExport)
    //    table.table().container().appendChild(formExport);
    //}
    return table;
}

function addEditModel(iDTable, iDFrm, title, url, model, iDRow) {

    $('#heading-popup-' + iDTable).html(title);
    $('#' + iDFrm).attr("action", url);
    if (model !== null) {
        for (var propertyName in model) {
            var e = $('[name=' + capitalizeFirstLetter(propertyName) + ']');
            if ($(e).length > 0) {
                if (typeof $(e).attr("data-allowInput") === 'undefined') {
                    if ($(e).attr("hasImg") === 'true') {
                        $("#img-" + propertyName.toLowerCase() + "-" + iDFrm).prop("src", model[propertyName]);
                    } else {
                        var value = model[propertyName];
                        if (typeof value !== "undefined" && value !== null) {
                            value = $('<textarea />').html(model[propertyName].toString()).text();
                        }
                        $(e).val(value);

                        if (typeof $(e).attr("data-isSelect2") !== "undefined") {
                            $(e).attr("data-isSelect2", iDRow);
                        }
                    }
                }
            }
        }
    }

    $('#modal-add-edit-' + iDTable).modal("show");
}
$(document).ready(function () {
    $('.remove-image').click(function (e) {
        var formParent = $(this).closest('form')
        var elementFile = $(formParent).find('input[type=file]');
        if (typeof elementFile !== "undefined") {
            $(elementFile).val(null);
            var elememtImg = $(formParent).find('img');
            $(elememtImg).attr('src', '/images/no-image.png');
            showRemoveImage("#" + $(elememtImg).get(0).id);
            var elementPath2 = $(formParent).find('input[name=PicturePath2]');
            if (elementPath2.length == 0) {
                elementPath2 = $(formParent).find('input[name=ImagePath2]');
            }
            $(elementPath2).val('/images/no-image.png');
        }
    })
})