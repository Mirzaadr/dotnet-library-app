// ClientApp/read-port.js
// import fs from 'fs';
// import path from 'path';
const fs = require("fs");
const path = require("path");

const launchSettingsPath = path.resolve(
  __dirname,
  "../Properties/launchSettings.json"
);
const launchSettings = JSON.parse(fs.readFileSync(launchSettingsPath, "utf8"));

// Assume 'LibraryApp.Api' is the profile name. Change as needed.
const profile = launchSettings.profiles["http"];

if (!profile || !profile.applicationUrl) {
  throw new Error("Could not find applicationUrl in launchSettings.json");
}

const urls = profile.applicationUrl.split(";");
const httpsUrl = urls.find((url) => url.startsWith("https://"));
const httpUrl = urls.find((url) => url.startsWith("http://"));

const targetUrl = httpsUrl || httpUrl;

if (!targetUrl) {
  throw new Error("No valid URL found in launchSettings.json");
}

console.log(targetUrl); // This is used by Vite config
