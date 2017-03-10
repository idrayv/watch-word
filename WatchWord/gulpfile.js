var gulp = require('gulp'),
    Q = require('q'),
    ts = require('gulp-typescript'),
    rimraf = require('rimraf');

gulp.task('clean:lib', function (cb) {
    return rimraf('./wwwroot/lib/', cb);
});

gulp.task('clean:app', function (cb) {
    return rimraf('./wwwroot/app/', cb);
});

gulp.task('copy:lib', ['clean:lib'], function () {
    var libs = [
        "@angular",
        "systemjs",
        "core-js",
        "zone.js",
        "reflect-metadata",
        "rxjs"
    ];

    var promises = [];

    libs.forEach(function (lib) {
        var defer = Q.defer();
        var pipeline = gulp
            .src('node_modules/' + lib + '/**/*')
            .pipe(gulp.dest('./wwwroot/lib/' + lib));

        pipeline.on('end', function () {
            defer.resolve();
        });
        promises.push(defer.promise);
    });

    return Q.all(promises);
});

gulp.task('ts', ['clean:app'], function () {
    var tsProject = ts.createProject('tsconfig.json');
    var tsResult = gulp.src("Scripts/**/*.ts") // or tsProject.src() 
        .pipe(tsProject());

    return tsResult.js.pipe(gulp.dest('wwwroot'));
});

gulp.task('default', ['copy:lib', 'ts']);