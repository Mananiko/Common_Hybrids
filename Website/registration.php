<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <title>Master Project_Project</title>
    <meta name="language" content="en" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"
        integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
    <link rel="stylesheet" href="style.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/crypto-js/3.1.9-1/core.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/crypto-js/3.1.9-1/md5.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"
        integrity="sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk=" crossorigin="anonymous"></script>

</head>

<!-- <div id="identificator">  -->
<!-- <div class="style">  -->

<body>

    <div class="body">
        
    <?php include('includes/header.php') ?>
        <!-- Whole Page -->
        <div class="main">

            <!-- Left Side-->
            <div class="container-fluid images">
                <div class="row">
                    <?php include('includes/registration/registration.php') ?>
                    <!-- Left Side End-->




                    <!-- Right Side-->
                    <?php include('includes/registration/checkstatus.php') ?>
                   

                    <!-- Right Side End-->
                </div>



            </div>
            <!-- Whole Page End -->
        </div>



</body>
</html>