export function writeUserData(userData){
    localStorage.setItem('userData',JSON.stringify(userData));
    return;
}

export function readUserData(){
    return JSON.parse(localStorage.getItem('userData'));
}
