<? 
    $name = $_POST['name']; // получаем имя из формы
    $phone = $_POST['phone']; // получаем  из формы

    // оформление текста, который приходит получателю
    $send = "Name: ".$name." Phone: ".$phone;
    $to= "missnimira@yandex.ru"; // кому отправляем форму
    $from = "no-replay@mail.com"; // от кого отправлена форма
    $subject = "Заявка с сайта"; // тема сообщения
    mail($to, $subject, $send);
 
    // В элементе $_SERVER['HTTP_REFERER'] приводится адрес страницы, с которой посетитель пришёл на данную страницу
    $redir = $_SERVER['HTTP_REFERER'];
     
    // условия проверки с пересылкой на страницу с формой с добавление GET параметра,
    // который нужен, чтобы по нему, можно было выводить благодарственный текст
    if (strpos($redir, "mail=1") === false) $redir .= "?mail=1#contact";
    // функция перенаправления, в данном случае на страницу с формой
header("Location: $redir");
?>