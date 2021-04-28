<?php
$servername = "localhost";
$username = "mananikoDB";
$password = "mananikoDB1@3";
$dbname = "commonhybrid";


// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
  die("Connection failed: " . mysqli_connect_error());
}

$errormsg['Status'] = 0;
$errormsg['Id'] = "";

if(!isset($_POST["id"]) || empty($_POST["id"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["user_id"]) || empty($_POST["user_id"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["password"]) || empty($_POST["password"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["moves"]) || empty($_POST["moves"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

$password = $_POST["password"];
$user_id = $_POST["user_id"];
$moves = $_POST["moves"];
$row_id = $_POST["id"];

$sql = "SELECT * FROM groups WHERE id = '$row_id' AND password = '$password'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
    $msg = array();
    $msg['msg'] = "login Failed !!!";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}


$sql = "SELECT * FROM user_moves WHERE group_id = '$row_id' AND user_id = '$user_id'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
  if(mysqli_query($conn, "INSERT INTO user_moves (group_id, user_id, moves) VALUES ('$row_id', '$user_id','$moves') ")){
    die;
  }
  else{
    $sapi_type = php_sapi_name();
    if (substr($sapi_type, 0, 3) == 'cgi')
        header("Status: 404 Not Found");
    else
        header("HTTP/1.1 404 Not Found");
    die;
  }
  
}
else{
  if(mysqli_query($conn, "UPDATE user_moves SET moves = '$moves' WHERE group_id = '$row_id' AND user_id = '$user_id'")){
    die;
  }
  else{
    $sapi_type = php_sapi_name();
    if (substr($sapi_type, 0, 3) == 'cgi')
        header("Status: 404 Not Found");
    else
        header("HTTP/1.1 404 Not Found");
    die;
  }
  
}




?>