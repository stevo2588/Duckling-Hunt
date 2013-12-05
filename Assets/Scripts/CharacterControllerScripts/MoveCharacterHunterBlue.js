var speed : float = 2.0;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;

private var moveDirection : Vector3 = Vector3.zero;

function Update() {
    var controller : CharacterController = GetComponent(CharacterController);
    //if (controller.isGrounded) {
        // We are grounded, so recalculate
        // move direction directly from axes
        moveDirection = Vector3(Input.GetAxis("Horizontal_hunter_blue"), 0,
                                Input.GetAxis("Vertical_hunter_blue"));
        
        //handle orientation
        if (moveDirection != Vector3.zero) {
            var rotation = transform.rotation; 
            rotation.SetLookRotation(-moveDirection); 
            transform.rotation = rotation;
        }
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
    //}

    // Apply gravity
   // moveDirection.y -= gravity * Time.deltaTime;
    
    // Move the controller
    controller.Move(moveDirection * Time.deltaTime);
}
