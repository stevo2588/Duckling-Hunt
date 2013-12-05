var speed : float = .5;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;

private var moveDirection : Vector3 = Vector3.zero;

function Start() {
	GetComponent.<Animation>().wrapMode = WrapMode.Loop;

	GetComponent.<Animation>()["waddle"].layer = -1;
	GetComponent.<Animation>().SyncLayer(-1);

	// We are in full control here - don't let any other animations play when we start
	GetComponent.<Animation>().Stop();
}

function Update() {
    var controller : CharacterController = GetComponent(CharacterController);
    if (controller.isGrounded) {
        // We are grounded, so recalculate
        // move direction directly from axes
        moveDirection = Vector3(Input.GetAxis("Horizontal_mom_yellow"), 0,
                                Input.GetAxis("Vertical_mom_yellow"));
                                        
        //handle orientation
        if (moveDirection != Vector3.zero) {
            var rotation = transform.rotation;
            rotation.SetLookRotation(moveDirection);
            transform.rotation = rotation;
            transform.RotateAround(Vector3.up,90);
            
            GetComponent.<Animation>().Play("waddle");
        }
        else {
        	GetComponent.<Animation>().Stop();
       	}
                                
       //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        
        if (Input.GetButton ("Jump")) {
            moveDirection.y = jumpSpeed;
        }

    }

    // Apply gravity
    moveDirection.y -= gravity * Time.deltaTime;
    
    // Move the controller
    controller.Move(moveDirection * Time.deltaTime);
}
