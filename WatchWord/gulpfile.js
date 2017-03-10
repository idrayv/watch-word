var gulp = require('gulp'),
    Q = require('q'),
    ts = require('gulp-typescript'),
    less = require('gulp-less'),
    path = require('path'),
    rimraf = require('rimraf');

gulp.task('clean-lib', function (cb) {
    return rimraf('./wwwroot/lib/', cb);
});

gulp.task('clean-app', function (cb) {
    return rimraf('./wwwroot/app/', cb);
});

gulp.task('copy-lib', ['clean-lib'], function () {
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

gulp.task('copy-html', ['clean-app'], function () {
    return gulp.src('app/**/*.html')
        .pipe(gulp.dest('wwwroot/app/'));
});

gulp.task('less', ['copy-html'], function () {
    return gulp.src('app/less/**/*.less')
        .pipe(less({
        }))
        .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('build-app', ['less'], function () {
    var tsProject = ts.createProject('tsconfig.json');
    var tsResult = gulp.src("app/**/*.ts")
        .pipe(tsProject());

    return tsResult.js.pipe(gulp.dest('wwwroot/app'));
});

gulp.task('default', ['copy-lib', 'build-app']);