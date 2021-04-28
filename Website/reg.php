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

if(!isset($_POST["group"]) || empty($_POST["group"])){
    $msg = array();
    $msg['msg'] = "Group name is empty";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

if (!isset($_POST["email"]) || !filter_var($_POST["email"], FILTER_VALIDATE_EMAIL)) {
  $msg = array();
  $msg['msg'] = "Invalid email format";
  $msg['status'] = 0;
  echo json_encode($msg);
  die;
}


$group = $_POST["group"];
$emails[] =  $_POST["email"];

if(isset($_POST["emails"]) && !empty($_POST["emails"])){
    $emsS = explode(",", $_POST["emails"]);
    for($i = 0; $i < count($emsS); $i++){
        if(filter_var(trim($emsS[$i]), FILTER_VALIDATE_EMAIL)){
            $emails[] = trim($emsS[$i]);
        }
    }
}

$password = md5($group."|".implode($emails,'|')."|".time());


$sql = "SELECT * FROM groups WHERE name = '$group'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    $msg = array();
    $msg['msg'] = "Group name is already taken. Please choose different name !!!";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

$sql = "INSERT INTO groups (name, isActive, password, created_day)
VALUES ('$group', 0, '$password', NOW())";

$last_id = 0;

if (mysqli_query($conn, $sql)) {
  $last_id = mysqli_insert_id($conn);
} else {
    $msg = array();
    $msg['msg'] = "Group did not create. Please contact administrator !!!";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}
for($i = 0; $i < count($emails); $i++){
    if(!empty(trim($emails[$i]))){
        $eemm  = trim($emails[$i]);
        mysqli_query($conn, "INSERT INTO users (group_id, email, created_day)
        VALUES ('$last_id', '$eemm', NOW())");
    }
}

mysqli_close($conn);

$msg = array();
$msg['msg'] = "Data saved successfully !!! Please remember password displayed in the input bellow, unless you won't be able to access in the game !!!";
$msg['status'] = 1;
$msg['password'] = $password;
echo json_encode($msg);
die;

?>