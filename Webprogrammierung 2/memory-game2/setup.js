export async function getSetup(lines = 1) {
    try {
        // TODO: switch to setup.php when the server is ready to provide JSON data from the database
        const response = await fetch(`setup.php?lines=${lines}`);
        const json = await response.json();
        return json;
    } catch( error ) {
        console.log(error);
        return [
        ];
    }
}
