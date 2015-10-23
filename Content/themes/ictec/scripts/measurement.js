﻿$(document).ready(function () {
    $(".wpcf7-submit").click(function () {

        isError = false;

        thisButton = $(this);

        clientNameField = thisButton.parent().find("[name=clientName]");
        clientNameField.removeClass("error");
        nameChars = " -ёйцукенгшщзхъфывапролджэячсмитьбюЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ";
        nameString = clientNameField.val();
        if (nameString.length < 3) {
            clientNameField.addClass("error");
            isError = true;
        }
        else {
            i = 0;
            while (ch = nameString.substr(i, 1)) {
                if (nameChars.indexOf(ch) == -1) {
                    clientNameField.addClass("error");
                    isError = true;
                    break;
                }
                i++;
            }
        }

        clientPhoneField = thisButton.parent().find("[name=clientPhone]");
        clientPhoneField.removeClass("error");
        phoneChars = " +-()1234567890";
        phoneString = clientPhoneField.val();
        if (phoneString.length < 5) {
            clientPhoneField.addClass("error");
            isError = true;
        }
        else {
            i = 0;
            while (ch = phoneString.substr(i, 1)) {
                if (phoneChars.indexOf(ch) == -1) {
                    clientPhoneField.addClass("error");
                    isError = true;
                    break;
                }
                i++;
            }
        }

        if (!isError) {

            systemString =  thisButton.parent().find("[name=clientSystem]").val();
            pageTracker._trackEvent("Заявка на замер - отправка", systemString, phoneString + "|" + nameString, 1);
            $("#YandexMetrikaWrapper").attr("src", "/MeasurementSend");

            thisButton.attr("disabled", "disabled");

            $.post('/Home/Feedback',
            {
                system: systemString,
                name: nameString,
                phone: phoneString
            },
            function (data) {
                alert(data.Message);
                thisButton.removeAttr("disabled");
                $.fancybox.close();
            }, "json");

        }

    });

    $(".measurementAsk").click(function () {
        dynamicForm = $("#MeasurementFormDynamic");
        dynamicForm.find("[name=clientSystem]").val($(this).attr("_system"));
        $.fancybox(dynamicForm);
        pageTracker._trackEvent("Заявка на замер - открытие", $(this).attr("_system"), document.location.href, 1);
        $("#YandexMetrikaWrapper").attr("src", "/MeasurementOpen");
    })

});