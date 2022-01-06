"# DFG_Golf" 

To create a new hole:
	- Add Hole# Game Object
		- Add Active Hoel Script
			- Put in hole number, Par value, Active hole = false. Add hole number, par number,putt count tmp's from canvas
		- Input all cubes with either box colliders or Mes Colliders
	- Add Bounds
		- Add PlayerReset Script
		- Add Hole Script from Hole#
	- Add Hole Trigger
		- Is trigger true
		- Add hole number
		- Add name of Hole# for which hole is next
	- Add a cube called StartMat
		- Add box collider
	- Find previous holes holetrigger and add the new hole# to the object
