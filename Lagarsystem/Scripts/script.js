$(function () {

    $("#success-alert").hide();
    $("#fail-alert").hide();
    $("#general-alert").hide();

    var createModalSubmitClicked = function () {
        $('#createForm').submit();
    };

    var CreateFormSubmitted = function () {
        var $createForm = $('#createForm');

        console.log($createForm.valid());

        if (!$createForm.valid()) {
            return false;
        }

        var DataToPost = $createForm.serializeArray();
        DataToPost.push({ __RequestVerificationToken: $('#createForm input[name=__RequestVerificationToken]').val() });

        var formURL = $createForm.attr("action");

        $.ajax({
            type: "POST",
            url: formURL,
            data: DataToPost,
            success: function (data) {
                $('#createModal').modal('hide');

                $("#general-alert").html("<strong>" + data.message + "</strong>");
                $("#general-alert").addClass("alert-success");
                $("#general-alert").show();
                $("#general-alert").alert();
                $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#general-alert").slideUp(500);
                    $("#general-alert").removeClass("alert-success");
                    $("#general-alert").html("");
                });

                refreshItems();
            },
            error: function () {
                $('#createModal').modal('hide');

                $("#general-alert").html("<strong>Something went wrong!</strong>");
                $("#general-alert").addClass("alert-danger");
                $("#general-alert").show();
                $("#general-alert").alert();
                $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#general-alert").slideUp(500);
                    $("#general-alert").removeClass("alert-danger");
                    $("#general-alert").html("");
                });
            }
        });

        return false;
    };

    var editModalSubmitClicked = function () {
        $('#editForm').submit();
    };

    var removeModalSubmitClicked = function () {
        var $removeForm = $('#RemoveForm');
        var DataToPost = $removeForm.serializeArray();
        DataToPost.push({ __RequestVerificationToken: $('#editForm input[name=__RequestVerificationToken]').val() });

        var formURL = $removeForm.attr("action");

        $.ajax({
            type: "POST",
            url: formURL,
            data: DataToPost,
            success: function (data) {
                $('#removeModal').modal('hide');

                $("#general-alert").html("<strong>" + data.message + "</strong>");
                $("#general-alert").addClass("alert-success");
                $("#general-alert").show();
                $("#general-alert").alert();
                $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#general-alert").slideUp(500);
                    $("#general-alert").removeClass("alert-success");
                    $("#general-alert").html("");
                });

                refreshItems();
            },
            error: function () {
                $('#removeModal').modal('hide');

                $("#general-alert").html("<strong>Something went wrong!</strong>");
                $("#general-alert").addClass("alert-danger");
                $("#general-alert").show();
                $("#general-alert").alert();
                $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#general-alert").slideUp(500);
                    $("#general-alert").removeClass("alert-danger");
                    $("#general-alert").html("");
                });
            }
        });

        return false;
    };

    var refreshItems = function () {
        var $form = $("#searchForm");

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-script-target"));
            $target.replaceWith(data);
            $('.table').bootstrapTable();
        });

        return false;
    };

    var ShowItemsListAjax = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-script-target"));
            $target.replaceWith(data);
            $('.table').bootstrapTable();
        });
        return false;
    };
    /*
    $.getJSON('/Stock/Autocomplete', function (allData) {
        $("input[data-script-autocomplete]").typeahead({ source: allData });
    });
    
    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    //$('#result').html(result);
                    console.log(result);
                }
            });
        }
        return false;
    });
    /*
    var AutoCompleteAjax = new Bloodhound( {
        
        datumTokenizer: function (datum) {
            return Bloodhound.tokenizers.whitespace(datum.value);
        },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            wildcard: '%QUERY',
            url: $(this).attr("data-script-autocomplete"),
            transform: function (response) {
                // Map the remote source JSON array to a JavaScript object array
                return $.map(response.results, function (item) {
                    return {
                        value: item.Name
                    };
                });
            }
        }
    });*/

    /*
    $('.typeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 1

    },
        {
            display: 'value',
            source: AutoCompleteAjax
            //$.getJSON($("input[data-script-autocomplete]").attr("data-script-autocomplete"), )
        });*/

    var DetailsLinkClicked = function () {
        var $this = $(this);
        $this.popover('show');
    };

    var EditLinkClicked = function () {
        
        var $this = $(this);

        var options = {
            url: $this.attr("action"),
            type: $this.attr("method"),
            data: $this.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $('#editformView');
            $target.html(data);
            $('#editModal').modal('show');
            $('.table').bootstrapTable();
        });
        return false;
    };

    var RemoveButtonClicked = function () {
        var $modal = $(this);

        var options = {
            url: $modal.attr("action"),
            type: $modal.attr("method"),
            data: $modal.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $('#removeModal .modal-body');
            $target.html(data);
            $('#removeModal').modal('show');
            $('.table').bootstrapTable();
        });
        return false;
    };

    $('body').popover({
        selector: '.detailsLink',
        trigger: 'focus',
        viewport: 'body'
    });

    var getPage = function () {
        var $this = $(this);

        var options = {
            url: $this.attr('href'),
            type: 'get',
            data: $('#searchForm').serialize()
        };

        $.ajax(options).done(function (data) {
            var target = $this.parents("div.pagedList").attr("data-script-target");
            $(target).replaceWith(data);

            $('.table').bootstrapTable();
        });

        
        return false;
    };

    var EditFormSubmitted = function () {
        var $editForm = $('#editForm');
        if (!$editForm.valid()) {
            return false;
        }

        var DataToPost = $editForm.serializeArray();
        DataToPost.push({ __RequestVerificationToken: $('#editForm input[name=__RequestVerificationToken]').val() });

        var formURL = $editForm.attr("action");

        $.ajax({
            type: "POST",
            url: formURL,
            data: DataToPost,
            success: function (data) {
                $('#editModal').modal('hide');

                $("#general-alert").html("<strong>" + data.message + "</strong>");
                $("#general-alert").addClass("alert-success");
                $("#general-alert").show();
                $("#general-alert").alert();
                $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#general-alert").slideUp(500);
                    $("#general-alert").removeClass("alert-success");
                    $("#general-alert").html("");
                });

                refreshItems();
            },
            error: function () {
                $('#createModal').modal('hide');

                $("#general-alert").html("<strong>Something went wrong!</strong>");
                $("#general-alert").addClass("alert-danger");
                $("#general-alert").show();
                $("#general-alert").alert();
                $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#general-alert").slideUp(500);
                    $("#general-alert").removeClass("alert-danger");
                    $("#general-alert").html("");
                });
            }
        });

        return false;
    }



    $('body').on("click", ".pagedList a" ,getPage);
    $('body').on("click", ".editLink", EditLinkClicked);
    $('body').on("click", ".removeButton", RemoveButtonClicked);

    $("#removeModalSubmit").click(removeModalSubmitClicked);

    $('#editForm').submit(EditFormSubmitted);
    $("#editModalSubmit").click(editModalSubmitClicked);
    
    $('#createForm').submit(CreateFormSubmitted);
    //$("#createModalSubmit").click(createModalSubmitClicked);

    $("#searchForm").submit(ShowItemsListAjax);
    //$("[data-script-autocomplete]").each(AutoCompleteAjax);
})