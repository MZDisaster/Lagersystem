$(function() {

    $("#success-alert").hide();
    $("#fail-alert").hide();
    $("#general-alert").hide();

    $("#myWish").click(function showAlert() {
        $("#success-alert").alert();
        $("#success-alert").fadeTo(2000, 500).slideUp(500, function () {
            $("#success-alert").slideUp(500);
        });
    });

    var createModalSubmitClicked = function () {
        $('#CreateForm').submit();
    }

    var CreateFormSubmitted = function () {

        var DataToPost = $('#CreateForm').serializeArray();
        DataToPost.push({ __RequestVerificationToken: $('[name=__RequestVerificationToken]').val() });

        var formURL = $(this).attr("action");

        $.ajax({
            type: "POST",
            url: formURL,
            data: DataToPost,
            success: function () {
                $('#createModal').modal('hide');

                $("#general-alert").html("<strong>Item Created!</strong>");
                $("#general-alert").addClass("alert-success");
                $("#general-alert").show();
                $("#general-alert").alert();
                $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#general-alert").slideUp(500);
                    $("#general-alert").removeClass("alert-success");
                    $("#general-alert").html("");
                });

                var $form = $("form[data-script-ajax='true']");
                console.log($form);
                var options = {
                    url: $form.attr("action"),
                    type: $form.attr("method"),
                    data: $form.serialize()
                };

                $.ajax(options).done(function (data) {
                    var $target = $($form.attr("data-script-target"));
                    $target.replaceWith(data);
                    return data;
                });
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

        });
        return false;
    };
    
    $.getJSON('/Stock/Autocomplete', function (allData) {
        $("input[data-script-autocomplete]").typeahead({ source: allData });
    });
    /*
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
    }

    var DetailsLinkClicked = function () {
        $('#editModal').modal('show');

        var $modal = $(this);

        var options = {
            url: $modal.attr("action"),
            type: $modal.attr("method"),
            data: $modal.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $modal.children(".modal-body");
            $target.replaceWith(data);
        });
        return false;
    }


    $('.table').on("click", ".detailsLink", EditLinkClicked);
    $('.table').on("click", ".detailsLink", DetailsLinkClicked);
    $('#CreateForm').submit(CreateFormSubmitted);
    $("#createModalSubmit").click(createModalSubmitClicked);
    $("form[data-script-ajax='true']").submit(ShowItemsListAjax);
    //$("input[data-script-autocomplete]").each(AutoCompleteAjax);
})