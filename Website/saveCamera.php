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
if(!isset($_POST["x"]) || empty($_POST["x"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["y"]) || empty($_POST["y"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["z"]) || empty($_POST["z"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["cfow"]) || empty($_POST["cfow"])){
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

$password = $_POST["password"];
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


$data = json_encode(array($_POST["x"],$_POST["y"],$_POST["z"],$_POST["cfow"]));



$sql = "SELECT * FROM content WHERE group_id = '$row_id'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
  if(mysqli_query($conn, "INSERT INTO content (group_id, objects, cubes, camera, comments) VALUES ('$row_id', '','', '$data', '') ")){
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
  if(mysqli_query($conn, "UPDATE content SET camera = '$data' WHERE group_id = '$row_id'")){
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